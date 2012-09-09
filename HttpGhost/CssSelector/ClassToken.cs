using System.Collections.Generic;

namespace HttpGhost.CssSelector
{
    internal class ClassToken : Token
    {
        private readonly List<char> pattern;
        private readonly CssSelectorParser selectorParser;

        public ClassToken(List<char> pattern, CssSelectorParser selectorParser) : base(selectorParser.PreviousChar)
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