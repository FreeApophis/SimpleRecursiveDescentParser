using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FormelParser.Visitors
{
    public class MinimalParenthesisVisitor : INodeVisitor
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
            _resultBuilder.AppendFormat("-");
            op.Operand.Accept(this);
        }

        public void Visit(BinaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(PlusOperator op)
        {
            List<Type> parenthesisOperators = new List<Type> { typeof(MultiplicationOperator), typeof(DivisionOperator) };
            FormatBinaryOperator(op, ParenthesisNeeded(parenthesisOperators));

        }

        public void Visit(MultiplicationOperator op)
        {
            FormatBinaryOperator(op, false);
        }

        public void Visit(DivisionOperator op)
        {
            List<Type> parenthesisOperators = new List<Type> { typeof(DivisionOperator) };
            FormatBinaryOperator(op, ParenthesisNeeded(parenthesisOperators));
        }

        public void Visit(MinusOperator op)
        {
            List<Type> parenthesisOperators = new List<Type> { typeof(MinusOperator), typeof(MultiplicationOperator), typeof(DivisionOperator) };
            FormatBinaryOperator(op, ParenthesisNeeded(parenthesisOperators));
        }

        private bool ParenthesisNeeded(IEnumerable<Type> parenthesisOperators)
        {
            bool parenthesis = false;
            if (operators.Count > 0)
            {
                var parentOp = operators.Peek();
                foreach (Type op in parenthesisOperators)
                {
                    parenthesis = parenthesis || (parentOp.GetType() == op);
                }
            }

            return parenthesis;
        }

        private void FormatBinaryOperator(BinaryOperator op, bool withParenthesis)
        {
            operators.Push(op);
            if (withParenthesis) { _resultBuilder.Append("("); }
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat(op.ToString());
            op.RightOperand.Accept(this);
            if (withParenthesis) { _resultBuilder.Append(")"); }
            operators.Pop();
        }

        public void Visit(VariableNode op)
        {
            _resultBuilder.Append(op.Name);
        }

        public void Visit(FunctionNode op)
        {
            operators.Push(op);
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
            operators.Pop();
        }

        public string Result => _resultBuilder.ToString();
        private StringBuilder _resultBuilder = new StringBuilder();
        private Stack<IParseNode> operators = new Stack<IParseNode>();

    }
}
