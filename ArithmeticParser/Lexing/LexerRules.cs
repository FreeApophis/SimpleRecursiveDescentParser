using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ArithmeticParser.Tokens;
using Funcky.Extensions;

namespace ArithmeticParser.Lexing
{
    public class LexerRules
    {
        public IEnumerable<ILexerRule> GetRules()
        {
            yield return new LexerRule(char.IsWhiteSpace, reader => { reader.Read(); return new WhiteSpaceToken(); });
            yield return new LexerRule(c => char.IsDigit(c) || c == '.', reader => new NumberToken(ScanNumber(reader)));
            yield return new LexerRule(char.IsLetter, reader => new IdentifierToken(ScanIdentifier(reader)));
            yield return new LexerRule(c => c == '-', reader => { reader.Read(); return new MinusToken(); });
            yield return new LexerRule(c => c == '+', reader => { reader.Read(); return new PlusToken(); });
            yield return new LexerRule(c => c == '*', reader => { reader.Read(); return new MultiplicationToken(); });
            yield return new LexerRule(c => c == '/', reader => { reader.Read(); return new DivideToken(); });
            yield return new LexerRule(c => c == '%', reader => { reader.Read(); return new ModuloToken(); });
            yield return new LexerRule(c => c == '^', reader => { reader.Read(); return new PowerToken(); });
            yield return new LexerRule(c => c == '(', reader => { reader.Read(); return new OpenParenthesisToken(); });
            yield return new LexerRule(c => c == ')', reader => { reader.Read(); return new ClosedParenthesisToken(); });
            yield return new LexerRule(c => c == ',', reader => { reader.Read(); return new CommaToken(); });
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
                    if (decimalExists) throw new Exception("Multiple dots in decimal number");
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

    public class WhiteSpaceToken : IToken
    {
    }
}
