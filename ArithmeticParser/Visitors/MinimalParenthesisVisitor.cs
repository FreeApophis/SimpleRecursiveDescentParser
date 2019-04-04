using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors
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

        public void Visit(UnaryMinusOperator op)
        {
            _resultBuilder.AppendFormat("-");
            op.Operand.Accept(this);
        }

        public void Visit(BinaryOperator op)
        {
            bool withParenthesis = ParenthesisNeeded(op);

            _operators.Push(op);
            if (withParenthesis) { _resultBuilder.Append("("); }
            op.LeftOperand.Accept(this);
            _resultBuilder.AppendFormat(op.ToString());
            op.RightOperand.Accept(this);
            if (withParenthesis) { _resultBuilder.Append(")"); }
            _operators.Pop();
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

        public void Visit(ModuloOperator op)
        {
            Visit((BinaryOperator)op);
        }

        public void Visit(PowerOperator op)
        {
            Visit((BinaryOperator)op);
        }

        private bool ParenthesisNeeded(BinaryOperator currentOp)
        {
            if (_operators.Any() && _operators.Peek() is BinaryOperator parentOp)
            {
                if (IsAssociativityRelevant(currentOp, parentOp))
                {
                    return ReferenceEquals(currentOp, GetNonAssociativeNode(currentOp, parentOp));
                }

                return parentOp.Precedence > currentOp.Precedence;
            }

            return false;
        }

        private static bool IsAssociativityRelevant(BinaryOperator currentNode, BinaryOperator parentOp)
        {
            return !currentNode.Commutative
                   && parentOp.GetType() == currentNode.GetType();
        }

        private static IParseNode GetNonAssociativeNode(BinaryOperator currentNode, BinaryOperator parentOp)
        {
            return currentNode.Associativity == Associativity.Left
                ? parentOp.RightOperand
                : parentOp.LeftOperand;
        }

        public void Visit(VariableNode op)
        {
            _resultBuilder.Append(op.Name);
        }

        public void Visit(FunctionNode op)
        {
            _operators.Push(op);
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
            _operators.Pop();
        }

        public string Result => _resultBuilder.ToString();
        private readonly StringBuilder _resultBuilder = new StringBuilder();
        private readonly Stack<IParseNode> _operators = new Stack<IParseNode>();

        public void Clear()
        {
            _resultBuilder.Clear();
        }
    }
}
