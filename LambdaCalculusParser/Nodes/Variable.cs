using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Variable(string name, Option<int> deBruinIndex = default) : ILambdaExpression
{
    public string Name { get; } = name;

    public Option<int> DeBruinIndex { get; } = deBruinIndex;

    public Position Position { get; }

    public void Accept(ILambdaExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override string ToString()
    {
        return $"{nameof(Variable)}: {this.ToNormalForm()}";
    }
}