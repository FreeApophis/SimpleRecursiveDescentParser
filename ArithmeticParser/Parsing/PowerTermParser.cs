using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;
using Funcky.Lexer.Extensions;

namespace ArithmeticParser.Parsing;

public class PowerTermParser
{
    private readonly FactorParser _factorParser;

    public PowerTermParser(FactorParser factorParser)
        => _factorParser = factorParser;

    public IParseNode Parse(ILexemeWalker walker)
        => ParseNextPowerToken(walker, _factorParser.Parse(walker));

    private IParseNode ParseNextPowerToken(ILexemeWalker walker, IParseNode result)
        => walker.NextIs<PowerToken>()
            ? ParseNextPowerToken(walker, NextToken(walker, result, walker.Pop()))
            : result;

    private PowerOperator NextToken(ILexemeWalker walker, IParseNode result, Lexeme lexeme)
        => new(result, _factorParser.Parse(walker), lexeme.Position);
}