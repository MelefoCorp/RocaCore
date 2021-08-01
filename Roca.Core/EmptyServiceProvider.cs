using System;

namespace Roca.Core
{
    public class EmptyServiceProvider : IServiceProvider
    {
        public static readonly IServiceProvider Instance = new EmptyServiceProvider();
        public object? GetService(Type serviceType) => null;
    }
}
