using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class ModuloOperator : BinaryOperator
    {
        internal ModuloOperator(IParseNode leftOperand, IParseNode rightOperand) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Point)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "%";
        }
    }
}
