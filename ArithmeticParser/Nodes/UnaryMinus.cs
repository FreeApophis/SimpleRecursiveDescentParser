using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents an unary minus operator parse node of the abstract syntax tree (AST)
/// </summary>
public class UnaryMinusOperator : UnaryOperator
{
    internal UnaryMinusOperator(IParseNode operand, Position position) 
        : base(operand, position)
    {
    }

    /// <inheritdoc />
    public override void Accept(INodeVisitor visitor) 
        => visitor.Visit(this);

    /// <inheritdoc />
    public override string ToString()
        => "-";
}