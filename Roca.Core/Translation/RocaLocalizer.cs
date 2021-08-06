using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Roca.Core.Extensions;
using System.Collections.Generic;
using System.Globalization;

namespace Roca.Core.Translation
{
    public class RocaLocalizer : IStringLocalizer
    {
        [BsonId]
        public ObjectId Id { get; private init; } = ObjectId.GenerateNewId();
        public string Name { get; private init; }
        public Dictionary<string, Dictionary<CultureInfo, List<TranslatedText>>> Texts { get; private init; } = new();

        public RocaLocalizer(string name) => Name = name;

        public LocalizedString this[string name]
        {
            get
            {
                name = name.ToLowerInvariant();
                if (!Texts.TryGetValue(name, out var result))
                    return FallbackLocalized(name);
                if (!result.TryGetValue(CultureInfo.CurrentUICulture, out var value))
                    if (!result.TryGetValue(CultureInfo.GetCultureInfo("en-US"), out value))
                        return FallbackLocalized(name);
                return new LocalizedString(name, value.GetBestText() ?? $"[{name}]");
            }
        }

        public LocalizedString this[string name, params object[] args]
        {
            get
            {
                var culture = CultureInfo.CurrentUICulture;

                name = name.ToLowerInvariant();
                if (!Texts.TryGetValue(name, out var result))
                    return FallbackLocalized(name);
                if (!result.TryGetValue(culture, out var value))
                {
                    culture = CultureInfo.GetCultureInfo("en-US");
                    if (!result.TryGetValue(culture, out value))
                        return FallbackLocalized(name);
                }
                return new LocalizedString(name, string.Format(culture, value.GetBestText() ?? $"[{name}]", args));
            }
        }

        private LocalizedString FallbackLocalized(string name)
        {
            if (!Texts.TryGetValue(name, out var result))
            {
                Texts.Add(name, new());
                this.Save();
            }
            return new(name, $"[{name}]");
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => throw new System.NotImplementedException();
    }
}
