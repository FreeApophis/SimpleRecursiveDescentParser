using System;
using System.Text;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public class PrintVisitor : ILambdaExpressionVisitor
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Visit(Term lambdaExpression)
        {
            _stringBuilder.Append("λ");

            lambdaExpression.Argument.Accept(this);

            _stringBuilder.Append(".");
            _stringBuilder.Append("(");

            lambdaExpression.Expression.Accept(this);

            _stringBuilder.Append(")");
        }

        public void Visit(Application application)
        {
            application.Function.Accept(this);
            _stringBuilder.Append(" ");
            application.Argument.Accept(this);
        }

        public void Visit(Variable variable)
        {
            _stringBuilder.Append(variable.Name);
        }

        public string Result => _stringBuilder.ToString();
    }
}
