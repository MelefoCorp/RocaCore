using System;
using System.Threading.Tasks;

namespace Roca.Core.Interfaces
{
    public interface IService : IDisposable
    {
        public Task Enable();
        public Task Disable();
    }
}
