using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Abstraction(Variable argument, ILambdaExpression expression) : ILambdaExpression
{
    public Variable Argument { get; } = argument;

    public ILambdaExpression Expression { get; } = expression;

    public Position Position { get; }

    public void Accept(ILambdaExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override string ToString()
    {
        return $"{nameof(Abstraction)}: {this.ToNormalForm()}";
    }
}