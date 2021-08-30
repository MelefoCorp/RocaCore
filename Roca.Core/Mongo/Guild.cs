using MongoDB.Driver;
using Roca.Core.Accounts;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using Discord;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static IMongoCollection<GuildAccount>? _guildsCollection;
        private static ConcurrentDictionary<ulong, GuildAccount> _guildsCache = new();

        private static void InitializeGuilds()
        {
            _guildsCollection = _database!.GetCollection<GuildAccount>("Guild");
            _guildsCache = new();
        }

        public static GuildAccount GetAccount(this IGuild guild, bool cache = true)
        {
            if (_guildsCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");

            if (cache && _guildsCache.TryGetValue(guild.Id, out var account))
                return account;

            var cursor = _guildsCollection.Find(x => x.Id == guild.Id);
            account = cursor.SingleOrDefault() ?? new GuildAccount(guild.Id);

            _guildsCache.TryAdd(guild.Id, account);
            return account;
        }

        public static async Task Save(this GuildAccount account, CancellationToken cancellationToken = default)
        {
            if (_guildsCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _guildsCollection.ReplaceOneAsync(x => x.ObjectId == account.ObjectId, account, new ReplaceOptions
            {
                IsUpsert = true,
            }, cancellationToken).ConfigureAwait(false);
        }

        public static void Uncache(this GuildAccount account) => _guildsCache.TryRemove(account.Id, out _);

        public static async Task Delete(this GuildAccount account, CancellationToken cancellationToken = default)
        {
            if (_guildsCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _guildsCollection.DeleteOneAsync(x => x.ObjectId == account.ObjectId, cancellationToken).ConfigureAwait(false);
            account.Uncache();
        }
    }
}
