using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Parsing
{
    public class SpaceToken
    {
        private readonly List<char> pattern;
        private readonly SelectorParser selectorParser;

        public SpaceToken(List<char> pattern, SelectorParser selectorParser)
        {
            this.pattern = pattern;
            this.selectorParser = selectorParser;
        }

        public void ToXpath()
        {
            if (selectorParser.IsProcessingAttribute)
            {
                pattern.AddRange(selectorParser.EndProcessingAttributeWith);
                selectorParser.IsProcessingAttribute = false;
            }
        }
    }
}