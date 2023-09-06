using Funcky.Lexer;
using Funcky.Monads;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Variable : ILambdaExpression
{
    public Variable(string name, Option<int> deBruinIndex = default)
    {
        Name = name;
        DeBruinIndex = deBruinIndex;
    }

    public string Name { get; }

    public Option<int> DeBruinIndex { get; }

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