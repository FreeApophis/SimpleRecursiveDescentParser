using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class UnaryOperator : IParseNode
    {
        public IParseNode Operand { get; set; }

        internal UnaryOperator(IParseNode operand)
        {
            Operand = operand;

        }

        public virtual void Accept(INodeVisitor visitor)
        {
        }
    }
}