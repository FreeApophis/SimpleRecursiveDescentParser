using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public class Application : ILambdaExpression
{
    public Application(ILambdaExpression function, ILambdaExpression argument)
    {
        Function = function;
        Argument = argument;
    }

    public ILambdaExpression Function { get; }

    public ILambdaExpression Argument { get; }

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