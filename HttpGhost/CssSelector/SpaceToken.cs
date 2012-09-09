using System.Collections.Generic;

namespace HttpGhost.CssSelector
{
    internal class SpaceToken
    {
        private readonly List<char> pattern;
        private readonly CssSelectorParser selectorParser;

        public SpaceToken(List<char> pattern, CssSelectorParser selectorParser)
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