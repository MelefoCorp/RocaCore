using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Roca.Core.Extensions;
using System.Collections.Generic;
using System.Globalization;

namespace Roca.Core.Translation
{
    public class Rocalizer : IStringLocalizer
    {
        [BsonId]
        public ObjectId Id { get; private init; } = ObjectId.GenerateNewId();
        public string Name { get; private init; }
        public Dictionary<string, Dictionary<CultureInfo, List<TranslatedText>>> Texts { get; private init; } = new();

        public Rocalizer(string name) => Name = name;

        public LocalizedString this[string name] =>
            this[CultureInfo.CurrentUICulture, name];

        public LocalizedString this[string name, params object[] args] =>
            this[CultureInfo.CurrentUICulture, name, args];

        public LocalizedString this[CultureInfo culture, string name, params object[] args]
        {
            get
            {
                name = name.ToLowerInvariant();
                if (!Texts.TryGetValue(name, out var result))
                    return FallbackLocalized(name);
                if (result.TryGetValue(culture, out var value))
                    return new LocalizedString(name, string.Format(culture, value.GetBestText() ?? $"[{name}]", args));
                culture = CultureInfo.GetCultureInfo("en-US");
                return !result.TryGetValue(culture, out value)
                    ? FallbackLocalized(name)
                    : new LocalizedString(name, string.Format(culture, value.GetBestText() ?? $"[{name}]", args));
            }
        }

        private LocalizedString FallbackLocalized(string name)
        {
            if (Texts.TryGetValue(name, out _)) return new(name, $"[{name}]", true);
            Texts.Add(name, new());
            this.Save();
            return new(name, $"[{name}]", true);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => throw new System.NotImplementedException();
    }
}
