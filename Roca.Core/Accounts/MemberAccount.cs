
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Roca.Core.Accounts
{
    public class MemberAccount : IAccount
    {
        [BsonId]
        public ObjectId ObjectId { get; private set; }

        public DateTime CreationDate { get; private set; }
        public ulong Id { get; private set; }
        public ulong Guild { get; private set; }

        public MemberAccount(ulong id, ulong guild)
        {
            ObjectId = ObjectId.GenerateNewId();
            Id = id;
            Guild = guild;
            CreationDate = DateTime.UtcNow;
        }
    }
}
