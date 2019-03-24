using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class MinusOperator : BinaryOperator
    {
        internal MinusOperator(IParseNode leftOperand, IParseNode rightOperand) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Line)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "-";
        }
    }
}