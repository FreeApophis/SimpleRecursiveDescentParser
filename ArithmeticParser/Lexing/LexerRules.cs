using System;
using System.Collections.Generic;
using System.Text;
using apophis.Lexer;
using ArithmeticParser.Tokens;
using Funcky.Extensions;

namespace ArithmeticParser.Lexing
{
    public class LexerRules : ILexerRules
    {
        public IEnumerable<ILexerRule> GetRules()
        {
            yield return new LexerRule(char.IsWhiteSpace, reader => { reader.Read(); return new WhiteSpaceToken(); });
            yield return new LexerRule(c => char.IsDigit(c) || c == '.', reader => new NumberToken(ScanNumber(reader)));
            yield return new LexerRule(char.IsLetter, reader => new IdentifierToken(ScanIdentifier(reader)));
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

        private static double ScanNumber(ILexerReader reader)
        {
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


            var maybeDouble = stringBuilder.ToString().TryParseDouble();

            return maybeDouble.Match(
                none: () => throw new Exception("Could not parse number: " + stringBuilder),
                some: d => d
                );
        }

        private static string ScanIdentifier(ILexerReader reader)
        {
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(false, char.IsLetterOrDigit))
            {
                stringBuilder.Append(reader.Read().Match(' ', c => c));
            }

            return stringBuilder.ToString();
        }
    }
}
