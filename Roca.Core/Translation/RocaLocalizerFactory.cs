using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace Roca.Core.Translation
{
    public class RocaLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type source) => source.GetLocalizer();

        public IStringLocalizer Create(string name, string location) => throw new NotImplementedException();
    }
}
