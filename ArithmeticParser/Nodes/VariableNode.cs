using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents a variable parse node of the abstract syntax tree (AST).
/// </summary>
public class VariableNode : IParseNode
{
    public VariableNode(Lexeme lexeme)
    {
        if (lexeme is { Token: IdentifierToken identifier })
        {
            Name = identifier.Name;
            Position = lexeme.Position;
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <inheritdoc />
    public Position Position { get; }

    public string Name { get; }

    /// <inheritdoc />
    public void Accept(INodeVisitor visitor)
        => visitor.Visit(this);
}