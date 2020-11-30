using System.Text;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public class NormalFormVisitor : ILambdaExpressionVisitor
    {
        private readonly StringBuilder _stringBuilder = new();

        public void Visit(Abstraction lambdaExpression)
        {
            _stringBuilder.Append("(");
            _stringBuilder.Append("λ");

            lambdaExpression.Argument.Accept(this);

            _stringBuilder.Append(".");

            lambdaExpression.Expression.Accept(this);

            _stringBuilder.Append(")");
        }

        public void Visit(Application application)
        {
            _stringBuilder.Append("(");
            application.Function.Accept(this);
            _stringBuilder.Append(" ");
            application.Argument.Accept(this);
            _stringBuilder.Append(")");
        }

        public void Visit(Variable variable)
        {
            _stringBuilder.Append(variable.Name);
        }

        public string Result 
            => _stringBuilder.ToString();
    }
}
