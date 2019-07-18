using System.Globalization;
using apophis.Lexer;
using ArithmeticParser.Tokens;
using ArithmeticParser.Visitors;

namespace ArithmeticParser.Nodes
{
    public class NumberNode : IParseNode
    {
        internal NumberNode(Lexem lexem)
        {
            if (lexem.Token is NumberToken number)
            {
                Number = number.Value;
                Position = lexem.Position;
            }
        }

        /// <inheritdoc />
        public Position Position { get; }
        public double Number { get; }

        public virtual void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Number.ToString(CultureInfo.InvariantCulture);
        }
    }
}