using System.Diagnostics;
using System.Linq;
using ArithmeticParser.Tokens;
using Funcky.Monads;

namespace ArithmeticParser.Lexing
{
    public class SimpleLexerRule<TToken> : ILexerRule
        where TToken : IToken, new()
    {
        private readonly string _textSymbol;

        public SimpleLexerRule(string textSymbol)
        {
            _textSymbol = textSymbol;
        }

        public int Weight => _textSymbol.Length;
        public Option<IToken> Match(ILexerReader reader)
        {
            if (_textSymbol.Select((character, index) => new { character, index }).All(t => reader.Peek(t.index).Match(false, c => c == t.character)))
            {
                Debug.Assert(_textSymbol.Select(c => reader.Read()).All(c => true));
                return Option.Some(CreateToken());
            }

            return Option<IToken>.None();

        }

        private IToken CreateToken()
        {
            return new TToken();
        }
    }
}