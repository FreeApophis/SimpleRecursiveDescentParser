using System.Text;
using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors;

public class FullParenthesisVisitor : INodeVisitor
{
    private readonly StringBuilder _resultBuilder = new();

    public string Result => _resultBuilder.ToString();

    public void Visit(NumberNode number)
    {
        _resultBuilder.Append(number);
    }

    public void Visit(UnaryOperator op)
    {
        _resultBuilder.Append('(');
        _resultBuilder.Append(op);
        op.Operand.Accept(this);
        _resultBuilder.Append(')');
    }

    public void Visit(UnaryMinusOperator op)
    {
        Visit((UnaryOperator)op);
    }

    public void Visit(BinaryOperator op)
    {
        _resultBuilder.Append('(');
        op.LeftOperand.Accept(this);
        _resultBuilder.Append(op);
        op.RightOperand.Accept(this);
        _resultBuilder.Append(')');
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

    public void Visit(VariableNode op)
    {
        _resultBuilder.AppendFormat(op.Name);
    }

    public void Visit(FunctionNode op)
    {
        _resultBuilder.Append(op.Name);
        _resultBuilder.Append('(');

        _ = op.Parameters
            .WithLast()
            .Inspect(parameter => parameter.Value.Accept(this))
            .Where(parameter => parameter.IsLast)
            .Aggregate(_resultBuilder, (builder, _) => builder.Append(','));

        _resultBuilder.Append(')');
    }
}
