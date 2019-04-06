using System.Collections.Generic;

namespace ArithmeticParser.Lexing
{
    public interface ILexerRules
    {
        IEnumerable<ILexerRule> GetRules();
    }
}