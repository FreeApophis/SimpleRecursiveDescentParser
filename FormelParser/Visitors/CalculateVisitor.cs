using System;
using System.Collections.Generic;

namespace FormelParser.Visitors
{
    public class CalculateVisitor : INodeVisitor
    {
        public void Visit(NumberNode number)
        {
            _stack.Push(number.Number);
        }


        public void Visit(UnaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnaryMinus op)
        {
            op.Operand.Accept(this);

            _stack.Push(-(_stack.Pop()));
        }

        public void Visit(BinaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(PlusOperator op)
        {
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);

            _stack.Push(_stack.Pop() + _stack.Pop());
        }

        public void Visit(MinusOperator op)
        {
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);

            var temp = _stack.Pop();
            _stack.Push(_stack.Pop() - temp);
        }

        public void Visit(MultiplicationOperator op)
        {
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);

            _stack.Push(_stack.Pop() * _stack.Pop());
        }

        public void Visit(DivisionOperator op)
        {
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);

            var temp = _stack.Pop();
            _stack.Push(_stack.Pop() / temp);
        }

        public double Result => _stack.Peek();
        private Stack<double> _stack = new Stack<double>();
    }
}
