using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
    public class TokenWalker
    {
        private readonly Tokenizer _tokenizer;
        private readonly Func<IToken> _newEpsilonToken;
        private List<IToken> _tokens = new List<IToken>();
        private int _currentIndex;

        private bool ValidToken(int lookAhead = 0) => _currentIndex + lookAhead < _tokens.Count;

        public TokenWalker(Tokenizer tokenizer, Func<IToken> newEpsilonToken)
        {
            _tokenizer = tokenizer;
            _newEpsilonToken = newEpsilonToken;
        }

        public void Scan(string expression)
        {
            Scan(expression, t => t);
        }

        public void Scan(string expression, Func<IEnumerable<IToken>, IEnumerable<IToken>> postProcessTokens)
        {
            _currentIndex = 0;
            var tokens = postProcessTokens(_tokenizer.Scan(expression));

            _tokens = tokens.ToList();
        }


        public IToken Pop()
        {
            return ValidToken()
                ? _tokens[_currentIndex++]
                : _newEpsilonToken();
        }


        public IToken Peek(int lookAhead = 0)
        {
            Debug.Assert(lookAhead >= 0);

            return ValidToken(lookAhead)
                ? _tokens[_currentIndex + lookAhead]
                : _newEpsilonToken();
        }
    }
}
