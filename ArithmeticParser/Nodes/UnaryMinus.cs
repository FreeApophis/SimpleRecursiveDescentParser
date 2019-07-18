using apophis.Lexer;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    /// <summary>
    /// Represents an unary minus parse node of the abstract syntax tree (AST)
    /// </summary>
    public class UnaryMinusOperator : UnaryOperator
    {
        internal UnaryMinusOperator(IParseNode operand, Position position) 
            : base(operand, position)
        {
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
