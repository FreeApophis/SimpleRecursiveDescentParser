using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public interface ILambdaExpression
    {
        void Accept(ILambdaExpressionVisitor visitor);

        Position Position { get; }
    }
}