using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Abstraction : ILambdaExpression
{
    public Abstraction(Variable argument, ILambdaExpression expression)
    {
        Argument = argument;
        Expression = expression;
    }

    public Variable Argument { get; }

    public ILambdaExpression Expression { get; }

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