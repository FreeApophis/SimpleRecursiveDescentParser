using System;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Factor     := RealNumber | "(" Expression ") | Variable | Function "
    /// </summary>
    public class FactorParser
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

        public IParseNode Parse(ITokenWalker walker) 
            => walker.Peek().Token switch
            {
                NumberToken => CreateNumberNode(walker),
                IdentifierToken => ParseFunctionOrVariable(walker),
                _ => ParseParenthesisExpression(walker),
            };

        private IParseNode ParseFunctionOrVariable(ITokenWalker walker) 
            => walker.NextIs<OpenParenthesisToken>(1)
                ? _functionParser.Parse(walker)
                : _variableParser.Parse(walker);

        private IParseNode ParseParenthesisExpression(ITokenWalker walker)
        {
            walker.ExpectOpeningParenthesis();
            var result = _expressionParser.Parse(walker);
            walker.ExpectClosingParenthesis();

            return result;
        }

        private static NumberNode CreateNumberNode(ITokenWalker walker) 
            => walker.Pop() is { Token: NumberToken } lexeme
                ? new NumberNode(lexeme)
                : throw new Exception("Expecting Real number but got something else");
    }
}
