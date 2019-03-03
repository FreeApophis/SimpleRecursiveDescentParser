using FormelParser.Visitors;

namespace FormelParser
{
    public class NumberNode : IParseNode
    {
        internal NumberNode(double number)
        {
            Number = number;
        }
        public virtual void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double Number { get; private set; }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}