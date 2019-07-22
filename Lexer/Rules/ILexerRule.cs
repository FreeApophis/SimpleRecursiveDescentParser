using Funcky.Monads;

namespace apophis.Lexer.Rules
{
    public interface ILexerRule
    {
        int Weight { get; }
        Option<Lexem> Match(ILexerReader reader);
    }
}