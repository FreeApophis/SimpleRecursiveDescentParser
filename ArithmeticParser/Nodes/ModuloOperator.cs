using ArithmeticParser.Visitors;
using Funcky.Lexer;

namespace ArithmeticParser.Nodes
{
    public class ModuloOperator : BinaryOperator
    {
        internal ModuloOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Point, position)
        {
        }

        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
            => visitor.Visit(this);

        /// <inheritdoc />
        public override string ToString() 
            => "%";
    }
}
