using System.Collections.Generic;

namespace apophis.Lexer
{
    public interface ILexerRules
    {
        IEnumerable<ILexerRule> GetRules();
    }
}