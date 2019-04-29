using ArithmeticParser.Nodes;

namespace ArithmeticParser.Visitors
{
    public interface INodeVisitor
    {
        void Visit(NumberNode number);
        void Visit(UnaryOperator op);
        void Visit(UnaryMinusOperator op);
        void Visit(BinaryOperator op);
        void Visit(PlusOperator op);
        void Visit(MinusOperator op);
        void Visit(MultiplicationOperator op);
        void Visit(DivisionOperator op);
        void Visit(ModuloOperator op);
        void Visit(PowerOperator op);
        void Visit(VariableNode op);
        void Visit(FunctionNode op);
    }
}