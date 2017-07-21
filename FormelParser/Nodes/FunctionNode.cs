using System.Collections.Generic;
using FormelParser.Visitors;

namespace FormelParser
{
    public class FunctionNode : IParseNode
    {
        public List<IParseNode> Parameters { get; } = new List<IParseNode>();
        public string Name { get; }

        public FunctionNode(string name)
        {
            Name = name;
        }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
