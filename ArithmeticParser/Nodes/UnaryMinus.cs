using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class UnaryMinusOperator : UnaryOperator
    {
        internal UnaryMinusOperator(IParseNode operand) : base(operand)
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
