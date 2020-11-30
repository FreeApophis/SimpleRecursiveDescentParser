using System.Collections.Generic;
using System.Linq;
using System.Text;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public class GraphvizVisitor : ILambdaExpressionVisitor
    {
        private int _nodeId;
        private readonly Stack<int> _stack = new();
        private readonly StringBuilder _result = new();

        public string Result => $"graph G {{\n{_result}}}";
        public void Visit(Abstraction abstraction)
        {
            EmitGraphvizNodeBegin("Abstraction");
            abstraction.Argument.Accept(this);
            abstraction.Expression.Accept(this);
            EmitGraphvizNodeEnd();
        }

        public void Visit(Application application)
        {
            EmitGraphvizNodeBegin("Application");
            application.Argument.Accept(this);
            application.Function.Accept(this);
            EmitGraphvizNodeEnd();
        }

        public void Visit(Variable variable)
        {
            EmitGraphvizNodeBegin($"Variable: {variable.Name}");

            EmitGraphvizNodeEnd();
        }

        private void EmitGraphvizNodeBegin(string label)
        {
            _result.AppendLine($"    node{_nodeId} [label=\"{label}\", shape=rectangle];");
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
