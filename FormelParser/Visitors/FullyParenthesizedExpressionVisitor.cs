using System;
using System.Text;

namespace FormelParser.Visitors
{
    public class FullParenthesisVisitor : INodeVisitor
    {
        public void Visit(NumberNode number)
        {
            _resultBuilder.Append(number);
        }
        public void Visit(UnaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnaryMinus op)
        {
            _resultBuilder.AppendFormat("(-");
            op.Operand.Accept(this);
            _resultBuilder.AppendFormat(")");
        }

        public void Visit(BinaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(PlusOperator op)
        {
            _resultBuilder.AppendFormat("(");
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat("+");
            op.RightOperand.Accept(this);
            _resultBuilder.AppendFormat(")");
        }

        public void Visit(MinusOperator op)
        {
            _resultBuilder.AppendFormat("(");
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat("-");
            op.RightOperand.Accept(this);
            _resultBuilder.AppendFormat(")");
        }

        public void Visit(MultiplicationOperator op)
        {
            _resultBuilder.AppendFormat("(");
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat("*");
            op.RightOperand.Accept(this);
            _resultBuilder.AppendFormat(")");
        }

        public void Visit(DivisionOperator op)
        {
            _resultBuilder.AppendFormat("(");
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat("/");
            op.RightOperand.Accept(this);
            _resultBuilder.AppendFormat(")");
        }

        public string Result => _resultBuilder.ToString();
        private StringBuilder _resultBuilder = new StringBuilder();
    }
}
