using MongoDB.Driver;
using Roca.Core.Accounts;
using Roca.Core.Translation;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static IMongoCollection<RocaLocalizer>? _localizersCollection = null;
        private static ConcurrentDictionary<string, RocaLocalizer> _localizersCache = new();

        private static void InitiliazeLocalizers()
        {
            _localizersCollection = _database!.GetCollection<RocaLocalizer>("Localizers");
            _localizersCache = new();
        }

        public static RocaLocalizer GetLocalizer(this Type type, bool cache = true)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            if (cache && _localizersCache.TryGetValue(type.FullName!, out var localizer))
                return localizer;

            var cursor = _localizersCollection.Find(x => x.Name == type.FullName, null);
            localizer = cursor.SingleOrDefault() ?? new RocaLocalizer(type.FullName!);

            _localizersCache.TryAdd(type.FullName!, localizer);
            return localizer;
        }

        public static void Save(this RocaLocalizer account)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            _localizersCollection.ReplaceOne(x => x.Id == account.Id, account, new ReplaceOptions
            {
                IsUpsert = true,
            });
        }

        public static void Uncache(this RocaLocalizer account) => _localizersCache.TryRemove(account.Name, out _);

        public static void Delete(this RocaLocalizer account)
        {
            if (_localizersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            _localizersCollection.DeleteOne(x => x.Id == account.Id);
            account.Uncache();
        }
    }
}
