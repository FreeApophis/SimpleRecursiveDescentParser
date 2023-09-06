using System.Text.RegularExpressions;
using Funcky.Lexer;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Lexing;

/// <summary>
/// This class represents the necessary lexer rules for the lambda calculus parser.
/// </summary>
public static class LexerRules
{
    public static LexerRuleBook GetRules()
        => LexerRuleBook.Builder
            .AddRule(char.IsWhiteSpace, ScanWhiteSpace)
            .AddRule(c => c is '\\' or 'λ', CreateLambdaToken)
            .AddRule(IsAllowedForIdentifier, ScanIdentifier)
            .AddSimpleRule<DotToken>(".")
            .AddSimpleRule<OpenParenthesisToken>("(")
            .AddSimpleRule<ClosedParenthesisToken>(")")
            .WithPostProcess(lexemes => lexemes.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)))
            .WithEpsilonToken<EpsilonToken>()
            .Build();

    private static Lexeme CreateLambdaToken(ILexemeBuilder builder)
    {
        builder.Discard();

        return builder.Build(new LambdaToken());
    }

    private static Lexeme ScanWhiteSpace(ILexemeBuilder builder)
    {
        while (builder.Peek().Match(false, char.IsWhiteSpace))
        {
            // we are not interested in what kind of whitespace, so we just discard the result
            builder.Discard();
        }

        return builder.Build(new WhiteSpaceToken());
    }

    private static Lexeme ScanIdentifier(ILexemeBuilder builder)
    {
        while (builder.Peek().Match(none: false, some: IsAllowedForIdentifier))
        {
            builder.Retain();
        }

        return builder.Build(new IdentifierToken(builder.CurrentToken));
    }

    private static bool IsAllowedForIdentifier(char identifierCharacter)
    {
        var regex = new Regex(@"[a-z]");
        var match = regex.Match(identifierCharacter.ToString());

        return match.Success;
    }
}