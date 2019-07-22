using System.Collections.Generic;
using System.Linq;
using apophis.Lexer.Rules;

namespace apophis.Lexer.Test.LexerRules
{
    class EmptyRules : ILexerRules
    {
        public IEnumerable<ILexerRule> GetRules()
        {
            return Enumerable.Empty<ILexerRule>();
        }
    }
}
