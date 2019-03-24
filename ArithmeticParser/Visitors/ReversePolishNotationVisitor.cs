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
            _resultBuilder.Append(op);
        }

        public void Visit(UnaryMinusOperator op)
        {
            Visit((UnaryOperator)op);
        }

        public void Visit(BinaryOperator op)
        {
            op.LeftOperand.Accept(this);
            _resultBuilder.Append(" ");
            op.RightOperand.Accept(this);
            _resultBuilder.Append($" {op}");
        }

        public void Visit(PlusOperator op)
        {
            Visit((BinaryOperator)op);
        }

        public void Visit(MinusOperator op)
        {
            Visit((BinaryOperator)op);
        }

        public void Visit(MultiplicationOperator op)
        {
            Visit((BinaryOperator)op);
        }

        public void Visit(DivisionOperator op)
        {
            Visit((BinaryOperator)op);
        }

        public void Visit(PowerOperator op)
        {
            Visit((BinaryOperator)op);
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
