using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Tokens;
using Messerli.Lexer;
using Messerli.Lexer.Rules;
using static Funcky.Functional;

namespace LambdaCalculusParser.Lexing
{
    /// <summary>
    /// This class represents the necessary lexer rules for the lambda calculus parser
    /// </summary>
    public static class LexerRules
    {
        public static IEnumerable<ILexerRule> GetRules()
        {
            yield return new LexerRule(char.IsWhiteSpace, ScanWhiteSpace);
            yield return new LexerRule(c => c is '\\' or 'λ', CreateLambdaToken);
            yield return new LexerRule(IsAllowedForIdentifier, ScanIdentifier);
            yield return new SimpleLexerRule<DotToken>(".");
            yield return new SimpleLexerRule<OpenParenthesisToken>("(");
            yield return new SimpleLexerRule<ClosedParenthesisToken>(")");
        }

        private static Lexeme ScanWhiteSpace(ILexerReader reader)
        {
            var startPosition = reader.Position;

            while (reader.Peek().Match(false, char.IsWhiteSpace))
            {
                // we are not interested in what kind of whitespace, so we just discard the result
                reader.Read().AndThen(NoOperation);
            }

            return new Lexeme(new WhiteSpaceToken(), new Position(startPosition, reader.Position - startPosition));
        }

        private static Lexeme CreateLambdaToken(ILexerReader reader)
        {
            reader.Read().AndThen(NoOperation);

            return new Lexeme(new LambdaToken(), new Position(reader.Position, 1));
        }

        private static Lexeme ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(none: false, some: IsAllowedForIdentifier)) 
            {
                reader.Read().AndThen(stringBuilder.Append).AndThen(NoOperation);
            }

            return new Lexeme(new IdentifierToken(stringBuilder.ToString()), new Position(startPosition, reader.Position - startPosition));
        }

        private static bool IsAllowedForIdentifier(char identifierCharacter)
        {
            var regex = new Regex(@"[a-z]");
            var match = regex.Match(identifierCharacter.ToString());

            return match.Success;
        }
    }
}
