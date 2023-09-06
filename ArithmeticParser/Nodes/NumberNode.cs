using System.Globalization;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes;

public class NumberNode : IParseNode
{
    internal NumberNode(Lexeme lexeme)
    {
        if (lexeme is { Token: NumberToken number })
        {
            Number = number.Value;
            Position = lexeme.Position;
        }
    }

    /// <inheritdoc />
    public Position Position { get; }

    public double Number { get; }

    /// <inheritdoc />
    public virtual void Accept(INodeVisitor visitor)
        => visitor.Visit(this);

    /// <inheritdoc />
    public override string ToString()
        => Number.ToString(CultureInfo.InvariantCulture);
}