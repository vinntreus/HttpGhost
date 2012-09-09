using System;
using System.Collections.Generic;

namespace HttpGhost.CssSelector
{
    internal class CssSelectorParser
    {
        private string pattern;
        protected bool IsXpath { get; private set; }
        public bool IsProcessingAttribute { get; set; }
        public string EndProcessingAttributeWith { get; set; }
        public char PreviousChar { get; private set; }
        private readonly IDictionary<char, Action<List<char>, CssSelectorParser>> tokens = new Dictionary<char, Action<List<char>, CssSelectorParser>>
        {
            {'.', (pattern, me) => new ClassToken(pattern, me).ToXpath()},
            {'#', (pattern, me) => new IdToken(pattern, me).ToXpath()},
            {'>', (pattern, me) => new DescendentToken(pattern).ToXpath()},
            {' ', (pattern, me) => new SpaceToken(pattern, me).ToXpath()},
            {'[', (pattern, me) => new BracketToken(pattern, me).ToXpath()}
        };

        public CssSelectorParser(string pattern)
        {
            IsXpath = pattern.StartsWith("//", StringComparison.InvariantCulture);
            this.pattern = pattern;
        }

        public string ToXPath()
        {
            if (IsXpath)
                return pattern;
            
            var newPattern = new List<char> { '/', '/' };
            InitPreviousChar();
            AddEndingDelimiterToPattern();

            foreach (var currentChar in pattern)
            {
                if(tokens.ContainsKey(currentChar))
                {
                    tokens[currentChar](newPattern, this);
                }
                else
                {
                    new ElementToken(newPattern, currentChar, PreviousChar).ToXpath();
                }
                PreviousChar = currentChar;
            }

            return string.Join("", newPattern).TrimEnd();
        }

        private void AddEndingDelimiterToPattern()
        {
            pattern += " ";
        }

        private void InitPreviousChar()
        {
            PreviousChar = '/';
        }
    }
}