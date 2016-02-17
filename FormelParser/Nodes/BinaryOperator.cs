using FormelParser.Visitors;

namespace FormelParser
{
    public class BinaryOperator : IParseNode
    {
        internal BinaryOperator(IParseNode leftOperand, IParseNode rightOperand)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }
        public IParseNode LeftOperand { get; set; }
        public IParseNode RightOperand { get; set; }

        public virtual void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}