using System;
using System.Collections.Generic;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;
using Messerli.Lexer;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a function parse node of the abstract syntax tree (AST)
    /// </summary>

    public class FunctionNode : IParseNode
    {
        public FunctionNode(Lexeme lexeme)
        {
            if (lexeme.Token is IdentifierToken identifier)
            {
                Name = identifier.Name;
                Position = lexeme.Position;
            } else
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public Position Position { get; }

        public Lexeme? OpenParenthesis { get; set; }

        public Lexeme? ClosedParenthesis { get; set; }

        public List<IParseNode> Parameters { get; } = new();

        public string Name { get; }


        /// <inheritdoc />
        public void Accept(INodeVisitor visitor) 
            => visitor.Visit(this);

    }
}
