
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Roca.Core.Accounts
{
    public class GuildAccount : IAccount
    {
        [BsonId]
        public ObjectId ObjectId { get; private init; } = ObjectId.GenerateNewId();

        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public ulong Id { get; private init; }

        public GuildAccount(ulong id) => Id = id;
    }
}
