using System.Collections.Generic;

namespace HttpGhost.CssSelector
{
    internal class BracketToken : Token
    {
        private readonly List<char> pattern;

        public BracketToken(List<char> pattern, CssSelectorParser selectorParser) : base(selectorParser.PreviousChar)
        {
            this.pattern = pattern;
        }

        public void ToXpath()
        {
            if (!IsPreviousCharElement())
            {
                pattern.Add('*');
            }
            pattern.AddRange("[@");
        }
    }
}