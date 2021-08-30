using MongoDB.Driver;
using Roca.Core.Translation;
using System;
using System.Collections.Concurrent;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static IMongoCollection<Rocalizer>? _localizersCollection;
        private static ConcurrentDictionary<string, Rocalizer> _localizersCache = new();

        private static void InitializeLocalizers()
        {
            _localizersCollection = _database!.GetCollection<Rocalizer>("Localizers");
            _localizersCache = new();
        }

        public static Rocalizer GetLocalizer(this Type type, bool cache = true)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            if (cache && _localizersCache.TryGetValue(type.FullName!, out var localizer))
                return localizer;

            var cursor = _localizersCollection.Find(x => x.Name == type.FullName);
            localizer = cursor.SingleOrDefault() ?? new Rocalizer(type.FullName!);

            _localizersCache.TryAdd(type.FullName!, localizer);
            return localizer;
        }

        public static void Save(this Rocalizer account)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            _localizersCollection.ReplaceOne(x => x.Id == account.Id, account, new ReplaceOptions
            {
                IsUpsert = true,
            });
        }

        public static void Uncache(this Rocalizer account) => _localizersCache.TryRemove(account.Name, out _);

        public static void Delete(this Rocalizer account)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            _localizersCollection.DeleteOne(x => x.Id == account.Id);
            account.Uncache();
        }
    }
}
