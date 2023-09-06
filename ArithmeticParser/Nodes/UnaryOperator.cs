using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents a generic unary operator parse node of the abstract syntax tree (AST).
/// </summary>
public class UnaryOperator : IParseNode
{
    internal UnaryOperator(IParseNode operand, Position position)
    {
        Operand = operand;
        Position = position;
    }

    /// <inheritdoc />
    public Position Position { get; }

    public IParseNode Operand { get; set; }

    /// <inheritdoc />
    public virtual void Accept(INodeVisitor visitor)
    {
    }
}