using System;
using System.Collections.Generic;
using apophis.Lexer;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a function parse node of the abstract syntax tree (AST)
    /// </summary>

    public class FunctionNode : IParseNode
    {
        public FunctionNode(Lexem lexem)
        {
            if (lexem.Token is IdentifierToken identifier)
            {
                Name = identifier.Name;
                Position = lexem.Position;
            } else
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public Position Position { get; }

        public Lexem? OpenParenthesis { get; set; }

        public Lexem? ClosedParenthesis { get; set; }

        public List<IParseNode> Parameters { get; } = new();

        public string Name { get; }


        /// <inheritdoc />
        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
