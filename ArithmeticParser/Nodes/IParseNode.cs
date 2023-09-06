using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a generic parse node of the abstract syntax tree (AST)
    /// </summary>
    public interface IParseNode
    {
        /// <summary>
        /// Accept method of the visitor pattern (simulate double dispatch)
        /// </summary>
        /// <param name="visitor"></param>
        void Accept(INodeVisitor visitor);

        /// <summary>
        /// Represents the actual textual position of this Node
        /// </summary>
        Position Position { get; }
    }
}
