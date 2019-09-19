using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Application : ILambdaExpression
    {
        public ILambdaExpression Function { get; }
        public ILambdaExpression Argument { get; }

        public Application(ILambdaExpression function, ILambdaExpression argument)
        {
            Function = function;
            Argument = argument;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }

        public override string ToString()
        {
            return $"{nameof(Application)}: {this.ToNormalForm()}";
        }
    }
}