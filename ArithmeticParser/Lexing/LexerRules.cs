using ArithmeticParser.Tokens;
using Funcky.Lexer;
namespace ArithmeticParser.Lexing;

/// <summary>
/// This class represents the necessary lexer rules for the arithmetic parser.
/// </summary>
public static class LexerRules
{
    public static LexerRuleBook GetRules()
        => LexerRuleBook.Builder
            .AddRule(char.IsWhiteSpace, ScanWhiteSpace)
            .AddRule(IsNumberChar, ScanNumber)
            .AddRule(char.IsLetter, ScanIdentifier)
            .AddSimpleRule<MinusToken>("-")
            .AddSimpleRule<PlusToken>("+")
            .AddSimpleRule<MultiplicationToken>("*")
            .AddSimpleRule<DivideToken>("/")
            .AddSimpleRule<ModuloToken>("%")
            .AddSimpleRule<PowerToken>("^")
            .AddSimpleRule<OpenParenthesisToken>("(")
            .AddSimpleRule<ClosedParenthesisToken>(")")
            .AddSimpleRule<CommaToken>(",")
            .WithPostProcess(lexemes => lexemes.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)))
            .WithEpsilonToken<EpsilonToken>()
            .Build();

    private static Lexeme ScanWhiteSpace(ILexemeBuilder builder)
        => SkipWhiteSpace(builder)
            .Build(new WhiteSpaceToken());

    // we are not interested in what kind of whitespace, so we just discard the result
    private static ILexemeBuilder SkipWhiteSpace(ILexemeBuilder builder)
        => builder.Peek().Match(none: false, some: char.IsWhiteSpace)
            ? SkipWhiteSpace(builder.Discard())
            : builder;

    private static Lexeme ScanNumber(ILexemeBuilder builder)
        => ScanDigitsOrDot(builder).CurrentToken
            .ParseDoubleOrNone()
            .AndThen(number => builder.Build(new NumberToken(number)))
            .GetOrElse(() => throw new Exception("Could not parse number: " + builder.CurrentToken));

    private static ILexemeBuilder ScanDigitsOrDot(ILexemeBuilder builder)
        => builder.Peek() switch
        {
            ['.'] => ScanDigits(builder.Retain()),
            [var c] when char.IsDigit(c) => ScanDigitsOrDot(builder.Retain()),
            _ => builder,
        };

    private static ILexemeBuilder ScanDigits(ILexemeBuilder builder)
        => builder.Peek().Select(char.IsDigit).GetOrElse(false)
            ? ScanDigits(builder.Retain())
            : builder;

    private static Lexeme ScanIdentifier(ILexemeBuilder builder)
        => builder.Peek().Match(false, char.IsLetterOrDigit)
            ? ScanIdentifier(builder.Retain())
            : builder.Build(new IdentifierToken(builder.CurrentToken));

    private static bool IsNumberChar(char c)
        => char.IsDigit(c) || c is '.';
}