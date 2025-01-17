using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Application(ILambdaExpression function, ILambdaExpression argument) : ILambdaExpression
{
    public ILambdaExpression Function { get; } = function;

    public ILambdaExpression Argument { get; } = argument;

    public Position Position { get; }

    public void Accept(ILambdaExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override string ToString()
    {
        return $"{nameof(Application)}: {this.ToNormalForm()}";
    }
}