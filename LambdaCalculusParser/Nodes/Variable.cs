using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Variable : ILambdaExpression
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }
    }
}