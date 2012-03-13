using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation.Parse
{
    public class ElementToken
    {
        private readonly List<char> pattern;
        private readonly char currentChar;

        public ElementToken(List<char> pattern, char currentChar)
        {
            this.pattern = pattern;
            this.currentChar = currentChar;
        }

        public void ToXpath()
        {
            pattern.Add(currentChar);
        }
    }
}