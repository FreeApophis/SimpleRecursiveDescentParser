using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class PlusOperator : BinaryOperator
    {
        internal PlusOperator(IParseNode leftOperand, IParseNode rightOperand) :
            base(leftOperand, rightOperand, Associativity.Left, true, Precedence.Line)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "+";
        }
    }
}