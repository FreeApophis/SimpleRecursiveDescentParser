using System;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Factor     := RealNumber | "(" Expression ") | Variable | Function "
    /// </summary>
    public class FactorParser : IParser
    {
        private readonly ExpressionParser _expressionParser;
        private readonly VariableParser _variableParser;
        private readonly FunctionParser _functionParser;


        public FactorParser(ExpressionParser expressionParser, VariableParser variableParser, FunctionParser functionParser)
        {
            _expressionParser = expressionParser;
            _variableParser = variableParser;
            _functionParser = functionParser;
        }

        /// <summary>
        /// Overloaded Parse function to parse a Factor
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
        {
            if (walker.NextIs<NumberToken>())
            {
                return new NumberNode(GetNumber(walker));
            }

            if (walker.NextIs<IdentifierToken>())
            {
                return walker.NextIs<OpenParenthesisToken>(1)
                    ? _functionParser.Parse(walker)
                    : _variableParser.Parse(walker);
            }

            walker.ExpectOpeningParenthesis();
            var result = _expressionParser.Parse(walker);
            walker.ExpectClosingParenthesis();

            return result;
        }

        private static double GetNumber(TokenWalker walker)
        {
            if (walker.Pop() is NumberToken number)
            {
                return number.Value;

            }

            throw new Exception("Expecting Real number but got something else");
        }
    }
}
