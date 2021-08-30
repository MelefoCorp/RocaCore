using Discord;
using MongoDB.Driver;
using Roca.Core.Accounts;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Roca.Core
{
    public static partial class Mongo
    {
        private static IMongoCollection<UserAccount>? _usersCollection;
        private static ConcurrentDictionary<ulong, UserAccount> _usersCache = new();

        private static void InitializeUsers()
        {
            _usersCollection = _database!.GetCollection<UserAccount>("Users");
            _usersCache = new();
        }

        public static UserAccount GetAccount(this IUser user, bool cache = true)
        {
            if (_usersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");

            if (cache && _usersCache.TryGetValue(user.Id, out var account))
                return account;

            var cursor = _usersCollection.Find(x => x.Id == user.Id);
            account = cursor.SingleOrDefault() ?? new UserAccount(user.Id);

            _usersCache.TryAdd(user.Id, account);
            return account;
        }

        public static async Task Save(this UserAccount account, CancellationToken cancellationToken = default)
        {
            if (_usersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _usersCollection.ReplaceOneAsync(x => x.ObjectId == account.ObjectId, account, new ReplaceOptions
            {
                IsUpsert = true,
            }, cancellationToken).ConfigureAwait(false);
        }

        public static void Uncache(this UserAccount account) => _usersCache.TryRemove(account.Id, out _);

        public static async Task Delete(this UserAccount account, CancellationToken cancellationToken = default)
        {
            if (_usersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _usersCollection.DeleteOneAsync(x => x.ObjectId == account.ObjectId, cancellationToken).ConfigureAwait(false);
            account.Uncache();
        }
    }
}
