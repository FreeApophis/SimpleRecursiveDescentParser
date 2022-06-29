using System;
using System.Collections.Generic;
using System.Text;
using ArithmeticParser.Tokens;
using Funcky.Extensions;
using Messerli.Lexer;
using Messerli.Lexer.Rules;
using static Funcky.Functional;

namespace ArithmeticParser.Lexing
{
    /// <summary>
    /// This class represents the necessary lexer rules for the arithmetic parser
    /// </summary>
    public static class LexerRules 
    {
        public static IEnumerable<ILexerRule> GetRules()
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

        private static Lexeme ScanWhiteSpace(ILexerReader reader)
        {
            var startPosition = reader.Position;

            while (reader.Peek().Match(none: false, some: char.IsWhiteSpace))
            {
                // we are not interested in what kind of whitespace, so we just discard the result
                reader.Read();
            }

            return new Lexeme(new WhiteSpaceToken(), new Position(startPosition, reader.Position - startPosition));
        }

        private static Lexeme ScanNumber(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            var decimalExists = false;
            while (reader.Peek().Match(none: false, some: char.IsDigit) || reader.Peek().Match(none: false, some: c => c == '.'))
            {
                var digit = reader.Read();
                var isDot =
                    from d in digit
                    select d == '.';

                if (isDot.Match(none: false, some: Identity))
                {
                    if (decimalExists)
                    {
                        throw new Exception("Multiple dots in decimal number");
                    }
                    decimalExists = true;
                }

                stringBuilder.Append(digit.Match(none: ' ', some: Identity));
            }


            var parsedDouble = stringBuilder.ToString().ParseDoubleOrNone().Match(none: () => throw new Exception("Could not parse number: " + stringBuilder), some: Identity);

            return new Lexeme(new NumberToken(parsedDouble), new Position(startPosition, reader.Position - startPosition));
        }

        private static Lexeme ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(false, char.IsLetterOrDigit))
            {
                stringBuilder.Append(reader.Read().Match(none: ' ', some: Identity));
            }

            return new Lexeme(new IdentifierToken(stringBuilder.ToString()), new Position(startPosition, reader.Position - startPosition));
        }
    }
}
