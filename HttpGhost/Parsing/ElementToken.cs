using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Parsing
{
    internal class ElementToken
    {
        private readonly List<char> pattern;
        private readonly char currentChar;
        private readonly char previousChar;

        public ElementToken(List<char> pattern, char currentChar, char previousChar)
        {
            this.pattern = pattern;
            this.currentChar = currentChar;
            this.previousChar = previousChar;
        }

        public void ToXpath()
        {
            if (pattern.Last() != '/' && previousChar == ' ')
                pattern.AddRange("//");
            pattern.Add(currentChar);
        }
    }
}