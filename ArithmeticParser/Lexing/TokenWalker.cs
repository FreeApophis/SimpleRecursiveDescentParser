using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Lexing
{
    public class TokenWalker
    {
        private readonly Tokenizer _tokenizer;
        private List<IToken> _tokens;
        private int _currentIndex;

        private bool ValidToken(int lookAhead = 0) => _currentIndex + lookAhead < _tokens.Count;

        public TokenWalker(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public void Scan(string expression)
        {
            _tokens = _tokenizer.Scan(expression).ToList();
            Reset();
        }

        private void Reset()
        {
            _currentIndex = 0;
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
