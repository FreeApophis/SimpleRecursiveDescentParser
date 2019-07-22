using System.Linq;
using apophis.Lexer.Tokens;
using Funcky.Monads;

namespace apophis.Lexer.Rules
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
                foreach (var _ in _textSymbol)
                {
                    reader.Read();
                }

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