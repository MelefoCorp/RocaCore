using Roca.Core.Interfaces;

namespace Roca.Core.Accounts
{
    public interface IAccount : IEntity
    {
        public ulong Id { get; }
    }
}
