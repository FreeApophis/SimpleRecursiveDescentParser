using System;
using System.Xml;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Parsing
{
    public class ApplicationParser : IParser
    {
        private readonly AtomParser _atomParser;

        public ApplicationParser(AtomParser atomParser)
        {
            _atomParser = atomParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Application := Application Atom | Atom
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<OpenParenthesisToken>())
            {
                return _atomParser.Parse(walker);
            }

            var application = Parse(walker);

            if (walker.Pop().Token is WhiteSpaceToken)
            {
                return new Application(application, _atomParser.Parse(walker));
            }

            throw new Exception("whitespace expected...");
        }
    }
}
