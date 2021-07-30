using Roca.Core.Translation;
using System.Collections.Generic;
using System.Linq;

namespace Roca.Core.Extensions
{
    public static class ListExtensions
    {
        public static string? GetBestText(this List<TranslatedText> list) =>
            list.SingleOrDefault(x => x.Approved)?.Content ??
            list.OrderByDescending(x => x.Upvotes.Count - x.Downvotes.Count).FirstOrDefault()?.Content;
    }
}
