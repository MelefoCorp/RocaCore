
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Roca.Core.Accounts
{
    public class GuildAccount : IAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; private init; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        public ulong Id { get; private init; }
        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public CultureInfo Language { get; private init; } = CultureInfo.GetCultureInfo("en-US");
        public ModerationConfig Moderation { get; private init; } = new();

        public GuildAccount(ulong id) => Id = id;
    }

    public class ModerationConfig
    {
        public ulong? Mute { get; set; }
        public ulong? Role { get; set; }
        public HelperConfig Helper { get; private init; } = new();
    }

    public class HelperConfig
    {
        public ulong? Role { get; set; }
        public ulong? Category { get; set; }
        public string? Info { get; set; }
        public TimeSpan? Duration { get; set; }
        public uint TicketsCount { get; set; } = 1;
        public Dictionary<ulong, Ticket> Tickets { get; private init; } = new();
    }

    public enum TicketStatus
    {
        Open,
        Closed
    }

    public class Ticket
    {
        public Ticket(uint id, ulong user)
        {
            Id = id;
            User = user;
            Status = TicketStatus.Open;
        }

        public uint Id { get; set; }
        public ulong User { get; private init; }
        public TicketStatus Status { get; set; }
    }
}
