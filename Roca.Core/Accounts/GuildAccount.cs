
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Globalization;

namespace Roca.Core.Accounts
{
    public class GuildAccount : IAccount
    {
        [BsonId]
        public ObjectId ObjectId { get; private init; } = ObjectId.GenerateNewId();

        public ulong Id { get; private init; }
        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public CultureInfo Language { get; private init; } = CultureInfo.GetCultureInfo("en-US");

        public GuildAccount(ulong id) => Id = id;
    }
}
