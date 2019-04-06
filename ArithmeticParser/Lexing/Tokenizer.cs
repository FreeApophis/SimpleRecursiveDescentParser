using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Lexing
{
    public class LexerRule
    {
        public Predicate<char> Predicate { get; }
        public Func<TextReader, IToken> CreateToken { get; }

        public LexerRule(Predicate<char> predicate, Func<TextReader, IToken> createToken)
        {
            Predicate = predicate;
            CreateToken = createToken;
        }
    }
    public class Tokenizer
    {
        private IEnumerable<LexerRule> LexerRules()
        {
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

        public IEnumerable<IToken> Scan(string expression)
        {
            var reader = new StringReader(expression);

            while (reader.Peek() != -1)
            {
                var c = (char)reader.Peek();

                if (char.IsWhiteSpace(c))
                {
                    reader.Read();
                    continue;

                }

                foreach (var lexerRule in LexerRules())
                {
                    if (lexerRule.Predicate(c))
                    {
                        yield return lexerRule.CreateToken(reader);
                    }
                }
            }
        }

        private static double ScanNumber(TextReader reader)
        {
            var stringBuilder = new StringBuilder();
            var decimalExists = false;
            while (char.IsDigit((char)reader.Peek()) || ((char)reader.Peek() == '.'))
            {
                var digit = (char)reader.Read();
                if (digit == '.')
                {
                    if (decimalExists) throw new Exception("Multiple dots in decimal number");
                    decimalExists = true;
                }
                stringBuilder.Append(digit);
            }

            if (!double.TryParse(stringBuilder.ToString(), out var res))
            {
                throw new Exception("Could not parse number: " + stringBuilder);
            }

            return res;
        }

        private static string ScanIdentifier(TextReader reader)
        {
            var stringBuilder = new StringBuilder();
            while (reader.Peek() != -1 && char.IsLetterOrDigit((char)reader.Peek()))
            {
                stringBuilder.Append((char)reader.Read());
            }

            return stringBuilder.ToString();
        }

    }
}
