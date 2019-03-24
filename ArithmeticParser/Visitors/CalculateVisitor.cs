using System;
using System.Collections.Generic;
using System.Reflection;
using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors
{
    public class CalculateVisitor : INodeVisitor
    {
        public Dictionary<string, double> Variables { get; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase) {
            { "e",  Math.E },
            { "pi",  Math.PI }
        };

        readonly Dictionary<string, MethodInfo> _functions = new Dictionary<string, MethodInfo>(StringComparer.OrdinalIgnoreCase) {
            { "sin",  typeof(Math).GetMethod("Sin") },
            { "cos",  typeof(Math).GetMethod("Cos") },
            { "tan",  typeof(Math).GetMethod("Tan") },
            { "sqrt",  typeof(Math).GetMethod("Sqrt") },
            { "pow",  typeof(Math).GetMethod("Pow") },
        };

        public void Visit(NumberNode number)
        {
            _stack.Push(number.Number);
        }


        public void Visit(UnaryOperator op)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnaryMinusOperator op)
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

        public void Visit(PowerOperator op)
        {
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);

            var temp = _stack.Pop();
            _stack.Push(Math.Pow(_stack.Pop(), temp));
        }


        public void Visit(VariableNode variable)
        {
            if (Variables.ContainsKey(variable.Name))
            {
                _stack.Push(Variables[variable.Name]);
            }
            else
            {
                throw new Exception("Unknown variable with name: " + variable.Name);
            }

        }

        public void Visit(FunctionNode function)
        {
            if (_functions.ContainsKey(function.Name))
            {
                List<object> parameters = new List<object>();
                foreach (IParseNode parameter in function.Parameters)
                {
                    parameter.Accept(this);
                    parameters.Add(_stack.Pop());
                }

                if (_functions[function.Name].Invoke(null, parameters.ToArray()) is double result)
                {
                    _stack.Push(result);
                }
            }
            else
            {
                throw new Exception("Unknown function with name: " + function.Name);
            }

        }

        public double Result => _stack.Peek();
        private readonly Stack<double> _stack = new Stack<double>();
    }
}
