using System;
using System.Collections.Generic;
using System.Text;
using apophis.Lexer;
using apophis.Lexer.Rules;
using ArithmeticParser.Tokens;
using Funcky.Extensions;

namespace ArithmeticParser.Lexing
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
            yield return new LexerRule(c => char.IsDigit(c) || c == '.', ScanNumber);
            yield return new LexerRule(char.IsLetter, ScanIdentifier);
            yield return new SimpleLexerRule<MinusToken>("-");
            yield return new SimpleLexerRule<PlusToken>("+");
            yield return new SimpleLexerRule<MultiplicationToken>("*");
            yield return new SimpleLexerRule<DivideToken>("/");
            yield return new SimpleLexerRule<ModuloToken>("%");
            yield return new SimpleLexerRule<PowerToken>("^");
            yield return new SimpleLexerRule<OpenParenthesisToken>("(");
            yield return new SimpleLexerRule<ClosedParenthesisToken>(")");
            yield return new SimpleLexerRule<CommaToken>(",");
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

        private static Lexem ScanNumber(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            var decimalExists = false;
            while (reader.Peek().Match(false, char.IsDigit) || reader.Peek().Match(false, c => c == '.'))
            {
                var digit = reader.Read();
                var isDot =
                    from d in digit
                    select d == '.';

                if (isDot.Match(false, p => p))
                {
                    if (decimalExists)
                    {
                        throw new Exception("Multiple dots in decimal number");
                    }
                    decimalExists = true;
                }

                stringBuilder.Append(digit.Match(' ', d => d));
            }


            var maybeDouble = stringBuilder.ToString().ParseDoubleOrNone();

            var parsedDouble = maybeDouble.Match(
                none: () => throw new Exception("Could not parse number: " + stringBuilder),
                some: d => d
                );

            return new Lexem(new NumberToken(parsedDouble), new Position(startPosition, reader.Position - startPosition));
        }

        private static Lexem ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(false, char.IsLetterOrDigit))
            {
                stringBuilder.Append(reader.Read().Match(' ', c => c));
            }

            return new Lexem(new IdentifierToken(stringBuilder.ToString()), new Position(startPosition, reader.Position - startPosition));
        }
    }
}
