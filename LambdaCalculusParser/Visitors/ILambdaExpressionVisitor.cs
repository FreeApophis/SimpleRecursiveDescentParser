using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public interface ILambdaExpressionVisitor
    {
        void Visit(Abstraction abstraction);

        void Visit(Application application);

        void Visit(Variable variable);

    }
}
