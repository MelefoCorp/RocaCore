using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace Roca.Core.Interfaces
{
    public interface IEntity
    {
        public ObjectId ObjectId { get; }
        public DateTime CreationDate { get; }
    }
}
