using System;
using apophis.Lexer;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a variable parse node of the abstract syntax tree (AST)
    /// </summary>
    public class VariableNode : IParseNode
    {
        public VariableNode(Lexem lexem)
        {
            if (lexem.Token is IdentifierToken identifier)
            {
                Name = identifier.Name;
                Position = lexem.Position;
            } 
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public Position Position { get; }

        public string Name { get; }

        /// <inheritdoc />
        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
