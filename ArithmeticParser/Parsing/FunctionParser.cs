using System;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Function   := Identifier "(" Expression { "," Expression } ")"
    /// </summary>
    public class FunctionParser : IParser
    {
        private readonly Parser _parser;

        public FunctionParser(Parser parser)
        {
            _parser = parser;
        }

        /// <summary>
        /// Overloaded Parse function to parse a Function
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
        {
            if (walker.Pop() is IdentifierToken identifier)
            {

                FunctionNode function = new FunctionNode(identifier.Name);

                // Pop opening parenthesis
                walker.Consume(typeof(OpenParenthesisToken));

                function.Parameters.Add(_parser.ParseExpression());
                while (walker.NextIs<CommaToken>())
                {
                    walker.Consume(typeof(CommaToken));
                    function.Parameters.Add(_parser.ParseExpression());
                }

                walker.Consume(typeof(ClosedParenthesisToken));
                return function;
            }

            throw new ArgumentNullException();
        }
    }
}