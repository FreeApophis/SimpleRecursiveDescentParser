using System.IO;
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
        public Option<IToken> Match(StreamReader reader)
        {
            var beforePosition = reader.BaseStream.Position;

            if (_textSymbol.All(c => c == (char)reader.Read()))
            {
                return Option.Some(CreateToken());
            }

            // token not consumed;
            reader.BaseStream.Position = beforePosition;
            return Option<IToken>.None();

        }

        private IToken CreateToken()
        {
            return new TToken();
        }
    }
}