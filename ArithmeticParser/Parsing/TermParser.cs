using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Funcky.Lexer;

namespace ArithmeticParser.Parsing;

/// <summary>
/// Parsing the following Production:
/// Term       := Factor { ( "*" | "/" ) Factor }
/// </summary>
public class TermParser(PowerTermParser powerTermParser)
{
    public IParseNode Parse(ILexemeWalker walker)
        => ParseNextDot(walker, powerTermParser.Parse(walker));

    private IParseNode ParseNextDot(ILexemeWalker walker, IParseNode result)
        => walker.NextIsDotOperator()
            ? ParseNextDot(walker, NextToken(walker, result, walker.Pop()))
            : result;

    private IParseNode NextToken(ILexemeWalker walker, IParseNode result, Lexeme lexeme)
        => lexeme switch
        {
            { Token: DivideToken } => new DivisionOperator(result, powerTermParser.Parse(walker), lexeme.Position),
            { Token: MultiplicationToken } => new MultiplicationOperator(result, powerTermParser.Parse(walker), lexeme.Position),
            { Token: ModuloToken } => new ModuloOperator(result, powerTermParser.Parse(walker), lexeme.Position),
            _ => result,
        };
}