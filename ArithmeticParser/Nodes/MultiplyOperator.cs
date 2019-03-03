using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class MultiplicationOperator : BinaryOperator
    {
        internal MultiplicationOperator(IParseNode leftOperand, IParseNode rightOperand) :
            base(leftOperand, rightOperand)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "*";
        }
    }
}