using Microsoft.Extensions.Localization;
using System;

namespace Roca.Core.Translation
{
    public class Localizer : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type source)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer Create(string name, string location)
        {
            throw new NotImplementedException();
        }
    }
}
