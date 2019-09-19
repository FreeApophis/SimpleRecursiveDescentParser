using apophis.Lexer;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Variable : ILambdaExpression
    {
        public string Name { get; }
        public int? DeBruinIndex { get; }

        public Variable(string name, int? deBruinIndex)
        {
            Name = name;
            DeBruinIndex = deBruinIndex;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }

        public override string ToString()
        {
            return $"{nameof(Variable)}: {this.ToNormalForm()}";
        }
    }
}