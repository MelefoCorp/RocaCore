using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roca.Core.Extensions
{
    public static class CommandInfoExtensions
    {
        public static string GetCustomId(this ComponentCommandInfo info)
            => info.ToString().Trim();
    }
}
