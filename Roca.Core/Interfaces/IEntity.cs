using System;

namespace Roca.Core.Interfaces
{
    public interface IEntity
    {
        public string ObjectId { get; }
        public DateTime CreationDate { get; }
    }
}
