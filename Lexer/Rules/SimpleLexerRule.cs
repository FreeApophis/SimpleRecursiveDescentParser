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

            if (IsSymbolMatchingReader(reader) && (IsOperator() || HasWordBoundary(reader)))
            {
                foreach (var _ in _textSymbol)
                {
                    reader.Read();
                }

                return Option.Some(CreateToken(startPosition));
            }

            return Option<Lexem>.None();
        }

        private bool HasWordBoundary(ILexerReader reader)
        {
            // we do not want to extract key words in the middle of a word, so a symbol must have ended.
            // Which means after a textsymbol must come something other than a digit or a letter.
            return reader.Peek(_textSymbol.Length).Match(true, NonLetterOrDigit);
        }

        private bool IsOperator()
        {
            return !_isTextSymbol;
        }

        private bool NonLetterOrDigit(char character)
        {
            return !char.IsLetterOrDigit(character);
        }

        private bool IsSymbolMatchingReader(ILexerReader reader)
        {
            return _textSymbol.Select((character, index) => new { character, index })
                                .All(t => reader.Peek(t.index).Match(false, c => c == t.character));
        }

        private Lexem CreateToken(int start)
        {
            return new Lexem(new TToken(), new Position(start, _textSymbol.Length));
        }
    }
}