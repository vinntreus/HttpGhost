using System;
using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation.Parse
{
    public class CssToXpath
    {
        private string pattern;
        protected bool IsXpath { get; private set; }
        public bool IsProcessingAttribute { get; set; }
        public string EndProcessingAttributeWith { get; set; }
        public char PreviousChar { get; private set; }
        private readonly IDictionary<char, Action<List<char>, CssToXpath>> tokens = new Dictionary<char, Action<List<char>, CssToXpath>>
                                                                                      {
            {'.', (pattern, me) => new ClassToken(pattern, me).ToXpath()},
            {'#', (pattern, me) => new IdToken(pattern, me).ToXpath()},
            {'>', (pattern, me) => new DescendentToken(pattern).ToXpath()},
            {' ', (pattern, me) => new SpaceToken(pattern, me).ToXpath()}
        };

        public CssToXpath(string pattern)
        {
            IsXpath = pattern.StartsWith("//", StringComparison.InvariantCulture);
            this.pattern = pattern;
        }


        public string Parse()
        {
            if (IsXpath)
                return pattern;
            
            var newPattern = new List<char> { '/', '/' };
            InitPreviousChar();
            AddSpaceToPattern();

            foreach (var currentChar in pattern)
            {
                if(tokens.ContainsKey(currentChar))
                {
                    tokens[currentChar](newPattern, this);
                }
                else
                {
                    new ElementToken(newPattern, currentChar).ToXpath();
                }
                PreviousChar = currentChar;
            }

            return string.Join("", newPattern).TrimEnd();
        }

        private void AddSpaceToPattern()
        {
            pattern += " ";
        }

        private void InitPreviousChar()
        {
            PreviousChar = '/';
        }
    }
}