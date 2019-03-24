using System;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Factor     := RealNumber | "(" Expression ") | Variable | Function "
    /// </summary>
    class FactorParser : IParser
    {
        private readonly VariableParser _variableParser;
        private readonly FunctionParser _functionParser;
        private readonly ExpressionParser _expressionParser;


        public FactorParser()
        {
            _variableParser = new VariableParser();
            _functionParser = new FunctionParser();
            _expressionParser = new ExpressionParser();
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

        private double GetNumber(TokenWalker walker)
        {
            if (walker.Pop() is NumberToken number)
            {
                return number.Value;

            }

            throw new Exception("Expecting Real number but got something else");
        }
    }
}
