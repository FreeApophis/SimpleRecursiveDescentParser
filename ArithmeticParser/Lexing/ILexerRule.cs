using ArithmeticParser.Tokens;
using Funcky.Monads;

namespace ArithmeticParser.Lexing
{
    public interface ILexerRule
    {
        int Weight { get; }
        Option<IToken> Match(ILexerReader reader);
    }
}