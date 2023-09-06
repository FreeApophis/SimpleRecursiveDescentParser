using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;
using Funcky.Lexer.Extensions;

namespace ArithmeticParser.Parsing;

/// <summary>
/// Parsing the following Production:
/// Expression := [ "-" ] Term { ("+" | "-") Term }
/// </summary>
public class ExpressionParser
{
    // Break the dependency cycle
    public TermParser? TermParser { get; set; }

    private TermParser SafeTermParser
        => TermParser ?? throw new Exception("Term Parser should be injected by the DI container, but is null");

    public IParseNode Parse(ILexemeWalker walker)
        => ParseNextTerm(walker, ParseFirstTerm(walker));

    private IParseNode ParseNextTerm(ILexemeWalker walker, IParseNode result)
        => walker.NextIsLineOperator()
            ? ParseNextTerm(walker, NextTerm(walker, result))
            : result;

    private IParseNode NextTerm(ILexemeWalker walker, IParseNode result)
        => walker.Pop() switch
        {
            { Token: MinusToken } lexeme => new MinusOperator(result, SafeTermParser.Parse(walker), lexeme.Position),
            { Token: PlusToken } lexeme => new PlusOperator(result, SafeTermParser.Parse(walker), lexeme.Position),
            _ => result,
        };

    private IParseNode ParseFirstTerm(ILexemeWalker walker)
        => walker.NextIs<MinusToken>()
            ? ParseUnaryMinus(walker)
            : SafeTermParser.Parse(walker);

    private IParseNode ParseUnaryMinus(ILexemeWalker walker)
        => ParseUnaryMinusTerm(walker, walker.Pop());

    private IParseNode ParseUnaryMinusTerm(ILexemeWalker walker, Lexeme lexeme)
        => new UnaryMinusOperator(SafeTermParser.Parse(walker), lexeme.Position);
}