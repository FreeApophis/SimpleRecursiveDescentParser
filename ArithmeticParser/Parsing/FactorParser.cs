using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;
using Funcky.Lexer.Extensions;

namespace ArithmeticParser.Parsing;

/// <summary>
/// Parsing the following Production:
/// Factor     := RealNumber | "(" Expression ") | Variable | Function "
/// </summary>
public class FactorParser(
    ExpressionParser expressionParser,
    VariableParser variableParser,
    FunctionParser functionParser)
{
    public IParseNode Parse(ILexemeWalker walker)
        => walker.Peek() switch
        {
            { Token: NumberToken } => CreateNumberNode(walker),
            { Token: IdentifierToken } => ParseFunctionOrVariable(walker),
            _ => ParseParenthesisExpression(walker),
        };

    private IParseNode ParseFunctionOrVariable(ILexemeWalker walker)
        => walker.NextIs<OpenParenthesisToken>(1)
            ? functionParser.Parse(walker)
            : variableParser.Parse(walker);

    private IParseNode ParseParenthesisExpression(ILexemeWalker walker)
    {
        walker.ExpectOpeningParenthesis();
        var result = expressionParser.Parse(walker);
        walker.ExpectClosingParenthesis();

        return result;
    }

    private static NumberNode CreateNumberNode(ILexemeWalker walker)
        => walker.Pop() is { Token: NumberToken } lexeme
            ? new NumberNode(lexeme)
            : throw new Exception("Expecting Real number but got something else");
}