using Funcky.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes;

public interface ILambdaExpression
{
    Position Position { get; }

    void Accept(ILambdaExpressionVisitor visitor);
}