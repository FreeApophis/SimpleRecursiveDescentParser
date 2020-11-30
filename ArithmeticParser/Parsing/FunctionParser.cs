using System;
using apophis.Lexer;
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
            var lexem = walker.Pop();
            if (lexem.Token is IdentifierToken)
            {
                var function = new FunctionNode(lexem);

                // Pop opening parenthesis
                function.OpenParenthesis = walker.Consume<OpenParenthesisToken>();

                function.Parameters.Add(_expressionParser.Parse(walker));
                while (walker.NextIs<CommaToken>())
                {
                    walker.Consume<CommaToken>();
                    function.Parameters.Add(_expressionParser.Parse(walker));
                }

                function.ClosedParenthesis = walker.Consume<ClosedParenthesisToken>();
                return function;
            }

            throw new ArgumentNullException();
        }
    }
}