using Microsoft.Extensions.Localization;
using System;

namespace Roca.Core.Translation
{
    public class RocaLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type source) => source.GetLocalizer();

        public IStringLocalizer Create(string name, string location) => throw new NotImplementedException();
    }
}
