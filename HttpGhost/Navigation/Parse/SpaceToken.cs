using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation.Parse
{
    public class SpaceToken
    {
        private readonly List<char> pattern;
        private readonly CssToXpath cssToXpath;

        public SpaceToken(List<char> pattern, CssToXpath cssToXpath)
        {
            this.pattern = pattern;
            this.cssToXpath = cssToXpath;
        }

        public void ToXpath()
        {
            if (cssToXpath.IsProcessingAttribute)
            {
                pattern.AddRange(cssToXpath.EndProcessingAttributeWith);
                cssToXpath.IsProcessingAttribute = false;
            }
        }
    }
}