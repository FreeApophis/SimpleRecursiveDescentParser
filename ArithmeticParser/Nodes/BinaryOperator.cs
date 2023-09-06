using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents a generic binary operator parse node of the abstract syntax tree (AST).
/// </summary>
public class BinaryOperator : IParseNode
{
    internal BinaryOperator(IParseNode leftOperand, IParseNode rightOperand, Associativity associativity, bool commutative, Precedence precedence, Position position)
    {
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Associativity = associativity;
        Commutative = commutative;
        Precedence = precedence;
        Position = position;
    }

    /// <inheritdoc />
    public Position Position { get; }

    public IParseNode LeftOperand { get; set; }

    public IParseNode RightOperand { get; set; }

    public Associativity Associativity { get; }

    public bool Commutative { get; }

    public Precedence Precedence { get; }

    /// <inheritdoc />
    public virtual void Accept(INodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
