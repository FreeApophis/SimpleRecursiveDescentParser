using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

public class PlusOperator : BinaryOperator
{
    internal PlusOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
        base(leftOperand, rightOperand, Associativity.Left, true, Precedence.Line, position)
    {
    }

    /// <inheritdoc />
    public override void Accept(INodeVisitor visitor) 
        => visitor.Visit(this);

    /// <inheritdoc />
    public override string ToString() 
        => "+";
}