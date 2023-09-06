using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents a power operator parse node of the abstract syntax tree (AST).
/// </summary>
public class PowerOperator : BinaryOperator
{
    internal PowerOperator(IParseNode leftOperand, IParseNode rightOperand, Position position)
        : base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Power, position)
    {
    }

    /// <inheritdoc />
    public override void Accept(INodeVisitor visitor)
        => visitor.Visit(this);

    /// <inheritdoc />
    public override string ToString()
        => "^";
}
