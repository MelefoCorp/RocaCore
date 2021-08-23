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
        private static IMongoCollection<MemberAccount>? _membersCollection = null;
        private static ConcurrentDictionary<ulong, MemberAccount> _membersCache = new();

        private static void InitiliazeMembers()
        {
            _membersCollection = _database!.GetCollection<MemberAccount>("Members");
            _membersCache = new();
        }

        public static MemberAccount GetAccount(this IGuildUser user, bool cache = true)
        {
            if (_membersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");

            if (cache && _membersCache.TryGetValue(user.Id, out var account))
                return account;

            var cursor = _membersCollection.Find(x => x.Id == user.Id);
            account = cursor.SingleOrDefault() ?? new MemberAccount(user.Id, user.Guild.Id);

            _membersCache.TryAdd(user.Id, account);
            return account;
        }

        public static async Task Save(this MemberAccount account, CancellationToken cancellationToken = default)
        {
            if (_membersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _membersCollection.ReplaceOneAsync(x => x.ObjectId == account.ObjectId, account, new ReplaceOptions
            {
                IsUpsert = true,
            }, cancellationToken).ConfigureAwait(false);
        }

        public static void Uncache(this MemberAccount account) => _membersCache.TryRemove(account.Id, out _);

        public static async Task Delete(this MemberAccount account, CancellationToken cancellationToken = default)
        {
            if (_membersCollection == null)
                throw new MongoException("The Mongo client has not been initialized.");
            await _membersCollection.DeleteOneAsync(x => x.ObjectId == account.ObjectId, cancellationToken).ConfigureAwait(false);
            account.Uncache();
        }
    }
}
