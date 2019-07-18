using apophis.Lexer;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class PlusOperator : BinaryOperator
    {
        internal PlusOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
            base(leftOperand, rightOperand, Associativity.Left, true, Precedence.Line, position)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "+";
        }
    }
}