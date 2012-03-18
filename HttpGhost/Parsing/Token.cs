using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HttpGhost.Parsing
{
    public class Token
    {
        protected char previousChar;

        public Token(char previousChar)
        {
            this.previousChar = previousChar;
        }

        protected bool IsPreviousCharSpace()
        {
            return previousChar == ' ';
        }

        protected bool IsPreviousCharElement()
        {
            return new Regex("[a-zA-Z]").IsMatch(previousChar.ToString(CultureInfo.InvariantCulture));
        }
    }
}