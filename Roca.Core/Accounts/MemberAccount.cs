
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Roca.Core.Accounts
{
    public class MemberAccount : IAccount
    {
        [BsonId]
        public ObjectId ObjectId { get; private init; } = ObjectId.GenerateNewId();

        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public ulong Id { get; private init; }
        public ulong Guild { get; private init; }

        public MemberAccount(ulong id, ulong guild)
        {
            Id = id;
            Guild = guild;
        }
    }
}
