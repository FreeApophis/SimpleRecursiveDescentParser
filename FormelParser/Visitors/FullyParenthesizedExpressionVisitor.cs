using System;
using System.Linq;
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

        public void Visit(UnaryMinusOperator op)
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

        public void Visit(VariableNode op)
        {
            _resultBuilder.AppendFormat(op.Name);
        }

        public void Visit(FunctionNode op)
        {
            _resultBuilder.Append(op.Name);
            _resultBuilder.Append("(");
            foreach (IParseNode parameter in op.Parameters)
            {
                parameter.Accept(this);

                if (parameter != op.Parameters.Last())
                {
                    _resultBuilder.Append(",");
                }
            }
            _resultBuilder.Append(")");

        }

        public string Result => _resultBuilder.ToString();
        private StringBuilder _resultBuilder = new StringBuilder();
    }
}
