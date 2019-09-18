using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public class ApplicationParser : IParser
    {
        public AtomParser AtomParser { get; set; }

        public ApplicationParser()
        {
        }

        /// <summary>
        /// Parsing the following Production:
        /// Application := Application Atom | Atom
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<OpenParenthesisToken>())
            {
                return AtomParser.Parse(walker);
            }

            throw new NotImplementedException();
        }
    }
}
