using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public class AtomParser : IParser
    {
        private readonly AbstractionParser _abstractionParser;

        public AtomParser(AbstractionParser abstractionParser)
        {
            _abstractionParser = abstractionParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Atom        := "(" Abstraction ")"
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<OpenParenthesisToken>())
            {
                walker.Consume<OpenParenthesisToken>();

                var result = _abstractionParser.Parse(walker);

                walker.Consume<ClosedParenthesisToken>();

                return result;
            }


            var token = walker.Pop().Token;
            if (token is IdentifierToken variableName)
            {
                return new Variable(variableName.Name);
            }

            throw new Exception($"Expecting an identifier in expression, instead got: {token}");
        }
    }
}