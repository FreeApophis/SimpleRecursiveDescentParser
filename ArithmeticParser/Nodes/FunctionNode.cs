using System.Collections.Immutable;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

/// <summary>
/// Represents a function parse node of the abstract syntax tree (AST)
/// </summary>
public record FunctionNode : IParseNode
{
    public FunctionNode(Lexeme lexeme, Lexeme openParenthesis, ImmutableList<IParseNode> parameters, Lexeme closedParenthesis)
    {
        if (lexeme.Token is IdentifierToken identifier)
        {
            Name = identifier.Name;
            Position = lexeme.Position;
            OpenParenthesisPosition = openParenthesis.Position;
            Parameters = parameters.ToImmutableList();
            ClosedParenthesisPosition = closedParenthesis.Position;
        } 
        else
        {
            throw new Exception("Function node must come from Identifier token.");
        }
    }

    /// <inheritdoc />
    public Position Position { get; }

    public Position OpenParenthesisPosition { get;  }

    public Position ClosedParenthesisPosition { get;  }

    public ImmutableList<IParseNode> Parameters { get; } 

    public string Name { get; }


    /// <inheritdoc />
    public void Accept(INodeVisitor visitor) 
        => visitor.Visit(this);

}