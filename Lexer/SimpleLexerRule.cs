using System.Diagnostics;
using System.Linq;
using apophis.Lexer.Tokens;
using Funcky.Monads;

namespace apophis.Lexer
{
    public class SimpleLexerRule<TToken> : ILexerRule
        where TToken : IToken, new()
    {
        private readonly string _textSymbol;
        private readonly bool _isTextSymbol;

        public SimpleLexerRule(string textSymbol)
        {
            _textSymbol = textSymbol;
            _isTextSymbol = textSymbol.All(char.IsLetter);
        }

        public int Weight => _textSymbol.Length;
        public Option<Lexem> Match(ILexerReader reader)
        {
            var startPosition = reader.Position;

            if (_textSymbol.Select((character, index) => new { character, index })
                    .All(t => reader.Peek(t.index).Match(false, c => c == t.character))
                && (!_isTextSymbol || reader.Peek(_textSymbol.Length).Match(true, char.IsWhiteSpace)))
            {
                Debug.Assert(_textSymbol.Select(c => reader.Read()).All(c => true));
                return Option.Some(CreateToken(startPosition));
            }

            return Option<Lexem>.None();

        }

        private Lexem CreateToken(int start)
        {
            return new Lexem(new TToken(), new Position(start, _textSymbol.Length));
        }
    }
}