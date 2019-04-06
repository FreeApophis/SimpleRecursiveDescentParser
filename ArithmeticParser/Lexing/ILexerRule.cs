using System.IO;
using ArithmeticParser.Tokens;
using Funcky.Monads;

namespace ArithmeticParser.Lexing
{
    public interface ILexerRule
    {
        Option<IToken> Match(TextReader reader);
    }
}