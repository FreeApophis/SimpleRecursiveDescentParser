using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Funcky.Monads;

namespace ArithmeticParser.Tokens
{
    public class TokenWalker
    {
        private readonly List<IToken> _tokens;
        private int _currentIndex;

        public bool ThereAreMoreTokens => _currentIndex < _tokens.Count;

        public TokenWalker(IEnumerable<IToken> tokens)
        {
            _tokens = tokens.ToList();
        }

        public IToken Pop()
        {
            if (_currentIndex < _tokens.Count)
            {
                return _tokens[_currentIndex++];
            }

            return new EpsilonToken();
        }


        public IToken Peek(int lookAhead = 0)
        {
            Debug.Assert(lookAhead >= 0);

            if (_currentIndex + lookAhead < _tokens.Count)
            {
                return _tokens[_currentIndex + lookAhead];
            }

            return new EpsilonToken();
        }
    }
}
