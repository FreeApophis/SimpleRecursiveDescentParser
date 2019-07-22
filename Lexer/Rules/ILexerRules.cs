using System.Collections.Generic;

namespace apophis.Lexer.Rules
{
    /// <summary>
    /// This interface represents a class which can return a full set rules for the lexer
    /// </summary>
    public interface ILexerRules
    {
        /// <summary>
        /// Function returns a full set of rules for the lexer
        /// </summary>
        /// <returns>lexer rules</returns>
        IEnumerable<ILexerRule> GetRules();
    }
}