using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using apophis.Lexer;
using apophis.Lexer.Rules;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Lexing
{
    /// <summary>
    /// This class represents the necessary lexer rules for the arithmetic parser
    /// </summary>
    public class LexerRules : ILexerRules
    {
        /// <inheritdoc />
        public IEnumerable<ILexerRule> GetRules()
        {
            yield return new LexerRule(char.IsWhiteSpace, ScanWhiteSpace);
            yield return new LexerRule(c => c == '\\' || c == 'λ', CreateLambdaToken);
            yield return new LexerRule(IsAllowedForIdentifier, ScanIdentifier);
            yield return new SimpleLexerRule<DotToken>(".");
            yield return new SimpleLexerRule<OpenParenthesisToken>("(");
            yield return new SimpleLexerRule<ClosedParenthesisToken>(")");
        }

        private static Lexem ScanWhiteSpace(ILexerReader reader)
        {
            var startPosition = reader.Position;

            while (reader.Peek().Match(false, char.IsWhiteSpace))
            {
                // we are not interested in what kind of whitespace, so we just discard the result
                reader.Read();
            }

            return new Lexem(new WhiteSpaceToken(), new Position(startPosition, reader.Position - startPosition));
        }

        private static Lexem CreateLambdaToken(ILexerReader reader)
        {
            var _ = reader.Read();

            return new Lexem(new LambdaToken(), new Position(reader.Position, 1));
        }

        private static Lexem ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(false, IsAllowedForIdentifier)) 
            {
                stringBuilder.Append(reader.Read().Match(' ', c => c));
            }

            return new Lexem(new IdentifierToken(stringBuilder.ToString()), new Position(startPosition, reader.Position - startPosition));
        }

        private static bool IsAllowedForIdentifier(char identifierCharacter)
        {
            var regex = new Regex(@"[a-z]");
            var match = regex.Match(identifierCharacter.ToString());

            return match.Success;

        }
    }
}
