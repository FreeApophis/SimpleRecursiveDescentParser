using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public interface ILambdaExpressionVisitor
    {
        void Visit(Term lambdaExpression);
        void Visit(Application application);
        void Visit(Variable variable);

    }
}
