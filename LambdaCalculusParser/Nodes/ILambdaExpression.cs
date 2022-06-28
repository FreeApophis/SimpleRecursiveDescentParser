using LambdaCalculusParser.Visitors;
using Messerli.Lexer;

namespace LambdaCalculusParser.Nodes
{
    public interface ILambdaExpression
    {
        void Accept(ILambdaExpressionVisitor visitor);

        Position Position { get; }
    }
}