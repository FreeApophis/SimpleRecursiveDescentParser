using System.Reflection;
using ArithmeticParser.Nodes;
using Funcky.Extensions;

namespace ArithmeticParser.Visitors;

public class CalculateVisitor : INodeVisitor
{
    private readonly Dictionary<string, MethodInfo> _functions = new(StringComparer.OrdinalIgnoreCase)
    {
        { "sin",  FromSystemMath("Sin") },
        { "cos",  FromSystemMath("Cos") },
        { "tan",  FromSystemMath("Tan") },
        { "sinh",  FromSystemMath("Sinh") },
        { "cosh",  FromSystemMath("Cosh") },
        { "tanh",  FromSystemMath("Tanh") },
        { "asin",  FromSystemMath("Asin") },
        { "acos",  FromSystemMath("Acos") },
        { "atan",  FromSystemMath("Atan") },
        { "atan2",  FromSystemMath("Atan2") },
        { "sqrt",  FromSystemMath("Sqrt") },
        { "pow",  FromSystemMath("Pow") },
        { "lb",  new Func<double, double>(BinaryLogarithm).Method },
        { "ln",  new Func<double, double>(Math.Log).Method },
        { "lg",  FromSystemMath("Log10") },
        { "log",  new Func<double, double, double>(Math.Log).Method },
    };

    private readonly Stack<double> _stack = new();

    public Dictionary<string, double> Variables { get; } = new(StringComparer.OrdinalIgnoreCase)
    {
        { "e",  Math.E },
        { "pi",  Math.PI },
    };

    public double Result => _stack.Peek();

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

        _stack.Push(-_stack.Pop());
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

    public void Visit(ModuloOperator op)
    {
        op.LeftOperand.Accept(this);
        op.RightOperand.Accept(this);

        var temp = _stack.Pop();
        _stack.Push(_stack.Pop() % temp);
    }

    public void Visit(PowerOperator op)
    {
        op.LeftOperand.Accept(this);
        op.RightOperand.Accept(this);

        var temp = _stack.Pop();
        _stack.Push(Math.Pow(_stack.Pop(), temp));
    }

    public void Visit(VariableNode variable)
        => Variables.GetValueOrNone(readOnlyKey: variable.Name)
            .Switch(
                none: () => throw new Exception("Unknown variable with name: " + variable.Name),
                some: _stack.Push);

    public void Visit(FunctionNode function)
        => _functions
            .GetValueOrNone(readOnlyKey: function.Name)
            .Switch(
                none: () => throw new Exception("Unknown function with name: " + function.Name),
                some: InvokeWithParameters(function.Parameters));

    private static MethodInfo FromSystemMath(string mathFunction)
        => typeof(Math).GetMethod(mathFunction)
           ?? throw new Exception($"function '{mathFunction} not found in System.Math'");

    private static double BinaryLogarithm(double value)
        => Math.Log(value, 2.0);

    private Action<MethodInfo> InvokeWithParameters(IEnumerable<IParseNode> parameterNodes)
        => methodInfo =>
        {
            var parameters = new List<object>();
            foreach (var parameter in parameterNodes)
            {
                parameter.Accept(this);
                parameters.Add(_stack.Pop());
            }

            if (methodInfo.Invoke(null, parameters.ToArray()) is double result)
            {
                _stack.Push(result);
            }
        };
}
