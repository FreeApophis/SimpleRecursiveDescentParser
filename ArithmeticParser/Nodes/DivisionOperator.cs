using apophis.Lexer;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents a division operator parse node of the abstract syntax tree (AST)
    /// </summary>
    public class DivisionOperator : BinaryOperator
    {
        internal DivisionOperator(IParseNode leftOperand, IParseNode rightOperand, Position position) :
            base(leftOperand, rightOperand, Associativity.Left, false, Precedence.Line, position)
        {
        }

        /// <inheritdoc />
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "/";
        }
    }
}