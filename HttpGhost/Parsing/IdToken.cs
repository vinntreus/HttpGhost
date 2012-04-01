using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Parsing
{
    internal class IdToken : Token
    {
        private readonly List<char> pattern;
        private readonly SelectorParser selectorParser;

        public IdToken(List<char> pattern, SelectorParser selectorParser) : base(selectorParser.PreviousChar)
        {
            this.pattern = pattern;
            this.selectorParser = selectorParser;
        }

        public void ToXpath()
        {
            selectorParser.IsProcessingAttribute = true;
            if (!IsPreviousCharElement())
            {
                pattern.Add('*');
            }
            pattern.AddRange("[@id=\"");
            selectorParser.EndProcessingAttributeWith = "\"]";
        }
    }
}