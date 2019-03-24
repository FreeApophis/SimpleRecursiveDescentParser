using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors
{
    public class GraphvizVisitor : INodeVisitor
    {
        private int _nodeId;
        private readonly Stack<int> _stack = new Stack<int>();
        private readonly StringBuilder _result = new StringBuilder();

        public string Result => $"graph G {{\n{_result}}}";

        public void Visit(NumberNode number)
        {
            EmitGraphvizNodeBegin(number.Number.ToString(CultureInfo.InvariantCulture));
            EmitGraphvizNodeEnd();
        }

        public void Visit(UnaryOperator op)
        {
            EmitGraphvizNodeBegin(op.ToString());
            op.Operand.Accept(this);
            EmitGraphvizNodeEnd();
        }

        public void Visit(UnaryMinusOperator op)
        {
            Visit((UnaryOperator)op);
        }

        public void Visit(BinaryOperator op)
        {
            EmitGraphvizNodeBegin(op.ToString());
            op.LeftOperand.Accept(this);
            op.RightOperand.Accept(this);
            EmitGraphvizNodeEnd();
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
            EmitGraphvizNodeBegin(op.Name);
            EmitGraphvizNodeEnd();
        }

        public void Visit(FunctionNode op)
        {
            string parameters = ParameterList(op);
            EmitGraphvizNodeBegin($"{op.Name}({parameters})");
            foreach (var parameter in op.Parameters)
            {
                parameter.Accept(this);
            }
            EmitGraphvizNodeEnd();
        }

        private static string ParameterList(FunctionNode op)
        {
            return string.Join(", ", op.Parameters.Select(p => "?"));
        }

        private void EmitGraphvizNodeBegin(string label)
        {
            _result.AppendLine($"    node{_nodeId} [label=\"{label}\", shape=circle];");
            if (_stack.Any())
            {
                _result.AppendLine($"    node{_stack.Peek()} -- node{_nodeId}");
            }
            _stack.Push(_nodeId);
            _nodeId++;
        }
        private void EmitGraphvizNodeEnd()
        {
            _stack.Pop();
        }

    }
}
