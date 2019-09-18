using System;
using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Term : ILambdaExpression
    {
        public Variable Argument { get; }
        public ILambdaExpression Expression { get; }

        public Term(Variable argument, ILambdaExpression expression)
        {
            Argument = argument;
            Expression = expression;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }
    }
}
