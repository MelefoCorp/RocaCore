using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Roca.Core.Accounts
{
    public class UserAccount : IAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; private init; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public ulong Id { get; private init; }

        public UserAccount(ulong id) => Id = id;
    }
}
