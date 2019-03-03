using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class VariableNode : IParseNode
    {
        public string Name { get; }

        public VariableNode(string name)
        {
            Name = name;
        }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
