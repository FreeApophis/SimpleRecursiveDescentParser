using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class PowerOperator : BinaryOperator
    {
        internal PowerOperator(IParseNode leftOperand, IParseNode rightOperand) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Power)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "^";
        }
    }
}
