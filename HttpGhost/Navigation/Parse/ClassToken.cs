using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation.Parse
{
    public class ClassToken : Token
    {
        private readonly List<char> pattern;
        private readonly CssToXpath cssToXpath;

        public ClassToken(List<char> pattern, CssToXpath cssToXpath) : base(cssToXpath.PreviousChar)
        {
            this.pattern = pattern;
            this.cssToXpath = cssToXpath;
        }

        public void ToXpath()
        {
            cssToXpath.IsProcessingAttribute = true;

            if (IsPreviousCharSpace())
            {
                pattern.Add('/');
            }
            if (!IsPreviousCharElement())
            {
                pattern.Add('*');
            }
            pattern.AddRange("[contains(@class,'");
            cssToXpath.EndProcessingAttributeWith = "')]";
        }
    }
}