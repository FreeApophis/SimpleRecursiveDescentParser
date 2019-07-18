using apophis.Lexer;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a generic parse node of the abstract syntax tree (AST)
    /// </summary>
    public interface IParseNode
    {
        void Accept(INodeVisitor visitor);

        /// <summary>
        /// Represents the actual textual position of this Node
        /// </summary>
        Position Position { get; }
    }
}
