using MongoDB.Bson;
using System;

namespace Roca.Core.Interfaces
{
    public interface IEntity
    {
        public ObjectId ObjectId { get; }
        public DateTime CreationDate { get; }
    }
}
