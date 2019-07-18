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
        private List<Lexem> _lexems = new List<Lexem>();
        private int _currentIndex;

        private bool ValidToken(int lookAhead = 0) => _currentIndex + lookAhead < _lexems.Count;

        public TokenWalker(Tokenizer tokenizer, Func<IToken> newEpsilonToken)
        {
            _tokenizer = tokenizer;
            _newEpsilonToken = newEpsilonToken;
        }

        public void Scan(string expression)
        {
            Scan(expression, t => t);
        }

        public void Scan(string expression, Func<IEnumerable<Lexem>, IEnumerable<Lexem>> postProcessTokens)
        {
            _currentIndex = 0;
            var lexems = postProcessTokens(_tokenizer.Scan(expression));

            _lexems = lexems.ToList();
        }


        public Lexem Pop()
        {
            return ValidToken()
                ? _lexems[_currentIndex++]
                : new Lexem(_newEpsilonToken(), new Position(0,0));
        }


        public Lexem Peek(int lookAhead = 0)
        {
            Debug.Assert(lookAhead >= 0);

            return ValidToken(lookAhead)
                ? _lexems[_currentIndex + lookAhead]
                : new Lexem(_newEpsilonToken(), new Position(0, 0));
        }
    }
}
