using apophis.Lexer;
using System;
using System.Linq;
using LambdaCalculusParser.Parsing;

namespace LambdaCalculusParser
{

    /// <summary>
    /// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
    ///
    /// Term        := Application | "λ" Identifier "." Term
    /// Application := Application Atom | Atom
    /// Atom        := "(" Term ")"
    /// Identifier  := [a-z]*
    /// </summary>
    public class Parser
    {
        private readonly TokenWalker _tokenWalker;
        private readonly TermParser _termParser;

        public Parser(TokenWalker tokenWalker, TermParser termParser)
        {
            _tokenWalker = tokenWalker;
            _termParser = termParser;
        }
        public IParseNode Parse(string expression)
        {
            _tokenWalker.Scan(expression, lexems => lexems);

            return Parse(_tokenWalker);
        }
        private IParseNode Parse(TokenWalker walker)
        {
            return _termParser.Parse(walker);
        }


    }
}
