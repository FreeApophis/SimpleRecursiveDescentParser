using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Expression := [ "-" ] Term { ("+" | "-") Term }
    /// </summary>
    public class ExpressionParser : IParser
    {
        // Break the dependency cycle
        public TermParser TermParser { get; set; }

        public IParseNode Parse(TokenWalker walker)
        {
            IParseNode result;
            if (walker.NextIs<MinusToken>())
            {
                walker.Pop();
                result = new UnaryMinusOperator(TermParser.Parse(walker));
            }
            else
            {
                result = TermParser.Parse(walker);
            }
            while (walker.NextIsLineOperator())
            {
                var op = walker.Pop();
                switch (op)
                {
                    case MinusToken _:
                        result = new MinusOperator(result, TermParser.Parse(walker));
                        break;
                    case PlusToken _:
                        result = new PlusOperator(result, TermParser.Parse(walker));
                        break;
                }
            }
            return result;
        }
    }
}
