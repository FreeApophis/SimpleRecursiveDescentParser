using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class BinaryOperator : IParseNode
    {
        internal BinaryOperator(IParseNode leftOperand, IParseNode rightOperand, Associativity associativity, bool commutative, Precedence precedence)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Associativity = associativity;
            Commutative = commutative;
            Precedence = precedence;
        }
        public IParseNode LeftOperand { get; set; }
        public IParseNode RightOperand { get; set; }
        public Associativity Associativity { get; }
        public bool Commutative { get; }
        public Precedence Precedence { get; }

        public virtual void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}