using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
    public class TokenWalker
    {
        private const int EpsilonLength = 0;
        private readonly Tokenizer _tokenizer;
        private readonly Func<IToken> _newEpsilonToken;
        private readonly LinePositionCalculator.Factory _newLinePositionCalculator;
        private List<Lexem> _lexems = new();
        private ILinePositionCalculator? _linePositionCalculator;

        private int _currentIndex;

        public TokenWalker(Tokenizer tokenizer, Func<IToken> newEpsilonToken, LinePositionCalculator.Factory newLinePositionCalculator) 
            => (_tokenizer, _newEpsilonToken, _newLinePositionCalculator) = (tokenizer, newEpsilonToken, newLinePositionCalculator);

        private Position EpsilonPosition
            => new(_lexems.Last().Position.EndPosition, EpsilonLength);

        public void Scan(string expression) 
            => Scan(expression, t => t);

        public void Scan(string expression, Func<IEnumerable<Lexem>, IEnumerable<Lexem>> postProcessTokens)
        {
            _currentIndex = 0;
            var unfilteredLexems = _tokenizer.Scan(expression);
            _linePositionCalculator = _newLinePositionCalculator(unfilteredLexems);
            _lexems = postProcessTokens(unfilteredLexems).ToList();
        }

        public Lexem Pop()
            => ValidToken()
                ? _lexems[_currentIndex++]
                : new Lexem(_newEpsilonToken(), EpsilonPosition);

        public Lexem Peek(int lookAhead = 0)
        {
            Debug.Assert(lookAhead >= 0, "a negative look ahead is not supported");

            return ValidToken(lookAhead)
                ? _lexems[_currentIndex + lookAhead]
                : new Lexem(_newEpsilonToken(), EpsilonPosition);
        }

        public LinePosition CalculateLinePosition(Lexem lexem)
            => _linePositionCalculator == null
                ? throw new Exception("Call Scan first before you try to calculate a position.")
                : _linePositionCalculator.CalculateLinePosition(lexem);

        private bool ValidToken(int lookAhead = 0) 
            => _currentIndex + lookAhead < _lexems.Count;
    }
}
