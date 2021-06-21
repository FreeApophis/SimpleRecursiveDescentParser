using apophis.Lexer;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class MinusOperator : BinaryOperator
    {
        internal MinusOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Line, position)
        {
        }

        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor) 
            => visitor.Visit(this);

        /// <inheritdoc />
        public override string ToString()
            => "-";
    }
}