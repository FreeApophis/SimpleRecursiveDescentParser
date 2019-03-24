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
        private readonly TermParser _termParser;

        public ExpressionParser()
        {
            _termParser = new TermParser(this);

        }

        public IParseNode Parse(TokenWalker walker)
        {
            IParseNode result;
            if (walker.NextIs<MinusToken>())
            {
                walker.Pop();
                result = new UnaryMinusOperator(_termParser.Parse(walker));
            }
            else
            {
                result = _termParser.Parse(walker);
            }
            while (walker.NextIsMinusOrPlus())
            {
                var op = walker.Pop();
                switch (op)
                {
                    case MinusToken _:
                        result = new MinusOperator(result, _termParser.Parse(walker));
                        break;
                    case PlusToken _:
                        result = new PlusOperator(result, _termParser.Parse(walker));
                        break;
                }
            }
            return result;
        } 
    }
}
