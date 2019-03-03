using System.Text;
using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors
{
    public class ReversePolishNotationVisitor : INodeVisitor
    {
        public void Visit(NumberNode number)
        {
            _resultBuilder.Append(number);
        }

        public void Visit(UnaryOperator op)
        {
            op.Operand.Accept(this);
            _resultBuilder.Append("unary");
        }

        public void Visit(UnaryMinusOperator op)
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

        public void Visit(VariableNode op)
        {
            _resultBuilder.Append(op.Name);
        }

        public void Visit(FunctionNode op)
        {
            foreach (IParseNode parameter in op.Parameters)
            {
                parameter.Accept(this);
                _resultBuilder.Append(" ");

            }
            _resultBuilder.Append(op.Name);
        }

        public string Result => _resultBuilder.ToString();
        private readonly StringBuilder _resultBuilder = new StringBuilder();
    }
}
