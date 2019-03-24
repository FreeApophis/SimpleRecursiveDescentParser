using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArithmeticParser.Tokens
{
    public class TokenWalker
    {
        private readonly List<IToken> _tokens;
        private int _currentIndex;

        private bool ValidToken(int lookAhead = 0) => _currentIndex + lookAhead < _tokens.Count;

        public TokenWalker(IEnumerable<IToken> tokens)
        {
            _tokens = tokens.ToList();
        }

        public IToken Pop()
        {
            return ValidToken()
                ? _tokens[_currentIndex++]
                : new EpsilonToken();
        }


        public IToken Peek(int lookAhead = 0)
        {
            Debug.Assert(lookAhead >= 0);

            return ValidToken(lookAhead)
                ? _tokens[_currentIndex + lookAhead]
                : new EpsilonToken();
        }
    }
}
