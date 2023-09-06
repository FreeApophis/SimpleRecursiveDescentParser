using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public static class LambdaExpressionExtension
{
    public static string ToNormalForm(this ILambdaExpression lambdaExpression)
    {
        var normalFormVisitor = new NormalFormVisitor();

        lambdaExpression.Accept(normalFormVisitor);

        return normalFormVisitor.Result;
    }
}