using System.Text;

namespace FormelParser.Visitors
{
    public class ReversePolishNotationVisitor : INodeVisitor
    {
        public void Visit(NumberNode number)
        {
            _resultBuilder.Append(number.ToString());
        }

        public void Visit(UnaryOperator op)
        {
            op.Operand.Accept(this);
            _resultBuilder.Append("unary");
        }

        public void Visit(UnaryMinus op)
        {
            _resultBuilder.Append("-");
            op.Operand.Accept(this);
        }

        public void Visit(BinaryOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append("binary");
        }

        public void Visit(PlusOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append("+");
        }

        public void Visit(MinusOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append("-");
        }

        public void Visit(MultiplicationOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append("*");
        }

        public void Visit(DivisionOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append("/");

        }

        public string Result => _resultBuilder.ToString();
        private StringBuilder _resultBuilder = new StringBuilder();
    }
}
