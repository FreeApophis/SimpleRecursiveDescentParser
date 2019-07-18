using System.Collections.Generic;
using System.Linq;

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
