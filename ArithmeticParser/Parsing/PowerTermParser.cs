using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Parsing
{
    public class PowerTermParser
    {
        private readonly FactorParser _factorParser;

        public PowerTermParser(FactorParser factorParser)
            => _factorParser = factorParser;

        public IParseNode Parse(ITokenWalker walker)
            => ParseNextPowerToken(walker, _factorParser.Parse(walker));

        private IParseNode ParseNextPowerToken(ITokenWalker walker, IParseNode result)
            => walker.NextIs<PowerToken>()
                ? ParseNextPowerToken(walker, NextToken(walker, result, walker.Pop()))
                : result;

        private PowerOperator NextToken(ITokenWalker walker, IParseNode result, Lexeme lexeme)
            => new(result, _factorParser.Parse(walker), lexeme.Position);
    }
}
