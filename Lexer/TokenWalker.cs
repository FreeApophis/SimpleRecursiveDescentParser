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
        private readonly Predicate<IToken> _filterTokens;
        private List<IToken> _tokens;
        private int _currentIndex;

        private bool ValidToken(int lookAhead = 0) => _currentIndex + lookAhead < _tokens.Count;

        public TokenWalker(Tokenizer tokenizer, Func<IToken> newEpsilonToken, Predicate<IToken> filterTokens)
        {
            _tokenizer = tokenizer;
            _newEpsilonToken = newEpsilonToken;
            _filterTokens = filterTokens;
        }

        public void Scan(string expression)
        {
            _currentIndex = 0;
            _tokens = _tokenizer
                .Scan(expression)
                .Where(t => _filterTokens(t))
                .ToList();
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
