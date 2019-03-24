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
        private readonly ExpressionParser _expressionParser;

        public FunctionParser(ExpressionParser expressionParser)
        {
            _expressionParser = expressionParser;
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
                walker.Consume<OpenParenthesisToken>();

                function.Parameters.Add(_expressionParser.Parse(walker));
                while (walker.NextIs<CommaToken>())
                {
                    walker.Consume<CommaToken>();
                    function.Parameters.Add(_expressionParser.Parse(walker));
                }

                walker.Consume<ClosedParenthesisToken>();
                return function;
            }

            throw new ArgumentNullException();
        }
    }
}