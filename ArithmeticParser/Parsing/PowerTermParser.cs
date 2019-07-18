using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    public class PowerTermParser : IParser
    {
        private readonly FactorParser _factorParser;

        public PowerTermParser(FactorParser factorParser)
        {
            _factorParser = factorParser;
        }
        public IParseNode Parse(TokenWalker walker)
        {
            var result = _factorParser.Parse(walker);

            while (walker.NextIs<PowerToken>())
            {
                var lexem = walker.Pop();
                result = new PowerOperator(result, _factorParser.Parse(walker), lexem.Position);
            }

            return result;
        }
    }
}
