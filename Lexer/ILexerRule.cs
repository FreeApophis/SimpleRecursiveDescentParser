using apophis.Lexer.Tokens;
using Funcky.Monads;

namespace apophis.Lexer
{
    public interface ILexerRule
    {
        int Weight { get; }
        Option<Lexem> Match(ILexerReader reader);
    }
}