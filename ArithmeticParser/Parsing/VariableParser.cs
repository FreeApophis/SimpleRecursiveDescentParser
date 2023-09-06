using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;

namespace ArithmeticParser.Parsing;

/// <summary>
/// Parsing the following Production:
/// Variable   := Identifier
/// </summary>
public class VariableParser
{
    public IParseNode Parse(ILexemeWalker walker) 
        => ParseIdentifier(walker.Pop());

    private static IParseNode ParseIdentifier(Lexeme lexeme)
        => lexeme.Token is IdentifierToken
            ? new VariableNode(lexeme)
            : throw new ArgumentNullException();
}