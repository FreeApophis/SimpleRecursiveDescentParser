using apophis.Lexer;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    public class PowerTermParser : IParser
    {
        private readonly FactorParser _factorParser;

        public PowerTermParser(FactorParser factorParser)
            => _factorParser = factorParser;
        public IParseNode Parse(TokenWalker walker)
            => ParseNextPowerToken(walker, _factorParser.Parse(walker));

        private IParseNode ParseNextPowerToken(TokenWalker walker, IParseNode result)
            => walker.NextIs<PowerToken>()
                ? ParseNextPowerToken(walker, NextToken(walker, result, walker.Pop()))
                : result;

        private PowerOperator NextToken(TokenWalker walker, IParseNode result, Lexem lexem)
            => new(result, _factorParser.Parse(walker), lexem.Position);
    }
}
