using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation.Parse
{
    public class IdToken : Token
    {
        private readonly List<char> pattern;
        private readonly CssToXpath cssToXpath;

        public IdToken(List<char> pattern, CssToXpath cssToXpath) : base(cssToXpath.PreviousChar)
        {
            this.pattern = pattern;
            this.cssToXpath = cssToXpath;
        }

        public void ToXpath()
        {
            cssToXpath.IsProcessingAttribute = true;
            if (!IsPreviousCharElement())
            {
                pattern.Add('*');
            }
            pattern.AddRange("[@id=\"");
            cssToXpath.EndProcessingAttributeWith = "\"]";
        }
    }
}