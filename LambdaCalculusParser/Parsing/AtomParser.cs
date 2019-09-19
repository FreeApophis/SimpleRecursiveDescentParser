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
        private readonly TermParser _termParser;

        public AtomParser(TermParser termParser)
        {
            _termParser = termParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Atom        := "(" Term ")"
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<OpenParenthesisToken>())
            {
                walker.Consume<OpenParenthesisToken>();

                var result = _termParser.Parse(walker);

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