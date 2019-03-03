using System.Globalization;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
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

        public double Number { get; }

        public override string ToString()
        {
            return Number.ToString(CultureInfo.InvariantCulture);
        }
    }
}