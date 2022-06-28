using ArithmeticParser.Visitors;
using Messerli.Lexer;

namespace ArithmeticParser.Nodes
{
    public class MultiplicationOperator : BinaryOperator
    {
        internal MultiplicationOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
            base(leftOperand, rightOperand, Associativity.Left, true, Precedence.Point, position)
        {
        }

        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor) 
            => visitor.Visit(this);

        /// <inheritdoc />
        public override string ToString()
            => "*";
    }
}