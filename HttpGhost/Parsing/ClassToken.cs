using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Parsing
{
    internal class ClassToken : Token
    {
        private readonly List<char> pattern;
        private readonly SelectorParser selectorParser;

        public ClassToken(List<char> pattern, SelectorParser selectorParser) : base(selectorParser.PreviousChar)
        {
            this.pattern = pattern;
            this.selectorParser = selectorParser;
        }

        public void ToXpath()
        {
            selectorParser.IsProcessingAttribute = true;

            if (IsPreviousCharSpace())
            {
                pattern.Add('/');
            }
            if (!IsPreviousCharElement())
            {
                pattern.Add('*');
            }
            pattern.AddRange("[contains(@class,'");
            selectorParser.EndProcessingAttributeWith = "')]";
        }
    }
}