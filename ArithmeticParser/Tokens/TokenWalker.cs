using System.Collections.Generic;
using System.Linq;

namespace ArithmeticParser.Tokens
{
    public class TokenWalker
    {
        private readonly List<Token> _tokens;
        private int _currentIndex;

        public bool ThereAreMoreTokens => _currentIndex < _tokens.Count;

        public TokenWalker(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToList();
        }

        public Token Pop()
        {
            return _tokens[_currentIndex++];
        }

        public Token Peek()
        {
            return _tokens[_currentIndex];
        }
    }
}
