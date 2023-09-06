using System.Collections.Immutable;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;
using Funcky.Lexer.Extensions;

namespace ArithmeticParser.Parsing;

/// <summary>
/// Parsing the following Production:
/// Function   := Identifier "(" Expression { "," Expression } ")"
/// </summary>
public class FunctionParser
{
    private readonly ExpressionParser _expressionParser;

    public FunctionParser(ExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public IParseNode Parse(ILexemeWalker walker) 
        => walker.Pop() is { Token: IdentifierToken } lexeme
            ? ParseFunction(walker, lexeme)
            : throw new ArgumentNullException();

    private IParseNode ParseFunction(ILexemeWalker walker, Lexeme functionLexeme)
    {
        var openParenthesis = walker.Consume<OpenParenthesisToken>();
        var parameters = ParseNextParameter(walker, ImmutableList.Create(_expressionParser.Parse(walker)));
        var closedParenthesis = walker.Consume<ClosedParenthesisToken>();

        return new FunctionNode(functionLexeme, openParenthesis, parameters, closedParenthesis);
    }

    private ImmutableList<IParseNode> ParseNextParameter(ILexemeWalker walker, ImmutableList<IParseNode> parameters) 
        => walker.NextIs<CommaToken>()
            ? ParseNextParameter(walker, ParseParameter(walker, parameters))
            : parameters;

    private ImmutableList<IParseNode> ParseParameter(ILexemeWalker walker, ImmutableList<IParseNode> parameters)
    {
        walker.Consume<CommaToken>();
        return parameters.Add(_expressionParser.Parse(walker));
    }
}