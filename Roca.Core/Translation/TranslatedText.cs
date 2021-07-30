using System;
using System.Collections.Generic;

namespace Roca.Core.Translation
{
    public class TranslatedText
    {
        public string Content { get; private init; }
        public ulong Creator { get; private init; }
        public DateTime CreationDate { get; private init; } = DateTime.UtcNow;
        public List<ulong> Upvotes { get; private init; } = new();
        public List<ulong> Downvotes { get; private init; } = new();
        public bool Approved { get; private init; }

        public TranslatedText(string content, ulong creator)
        {
            Content = content;
            Creator = creator;
        }

    }
}
