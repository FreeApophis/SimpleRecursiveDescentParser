using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public interface IParseNode
    {
        void Accept(INodeVisitor visitor);
    }
}
