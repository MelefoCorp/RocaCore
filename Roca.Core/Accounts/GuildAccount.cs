
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Roca.Core.Accounts
{
    public class GuildAccount : IAccount
    {
        [BsonId]
        public ObjectId ObjectId { get; private set; }

        public DateTime CreationDate { get; private set; }
        public ulong Id { get; private set; }

        public GuildAccount(ulong id)
        {
            ObjectId = ObjectId.GenerateNewId();
            Id = id;
            CreationDate = DateTime.UtcNow;
        }
    }
}
