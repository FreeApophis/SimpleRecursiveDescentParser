using FormelParser.Visitors;

namespace FormelParser
{
    public interface IParseNode
    {
        void Accept(INodeVisitor visitor);
    }
}
