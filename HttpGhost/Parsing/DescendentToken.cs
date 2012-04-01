using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Parsing
{
    internal class DescendentToken
    {
        private readonly List<char> pattern;

        public DescendentToken(List<char> pattern)
        {
            this.pattern = pattern;
        }

        public void ToXpath()
        {
            pattern.Add('/');
        }
    }
}