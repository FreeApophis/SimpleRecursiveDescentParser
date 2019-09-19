using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Abstraction : ILambdaExpression
    {
        public Variable Argument { get; }
        public ILambdaExpression Expression { get; }

        public Abstraction(Variable argument, ILambdaExpression expression)
        {
            Argument = argument;
            Expression = expression;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }

        public override string ToString()
        {
            return $"{nameof(Abstraction)}: {this.ToNormalForm()}";
        }
    }
}
