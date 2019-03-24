using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArithmeticParser.Tokens
{
    public class Tokenizer
    {
        private StringReader _reader;

        public IEnumerable<IToken> Scan(string expression)
        {
            _reader = new StringReader(expression);

            while (_reader.Peek() != -1)
            {
                var c = (char)_reader.Peek();
                if (char.IsWhiteSpace(c))
                {
                    _reader.Read();
                    continue;
                }

                if (char.IsDigit(c) || c == '.')
                {
                    var value = ParseNumber();
                    yield return new NumberToken(value);
                }
                else if (c == '-')
                {
                    yield return new MinusToken();
                    _reader.Read();
                }
                else if (c == '+')
                {
                    yield return new PlusToken();
                    _reader.Read();
                }
                else if (c == '*')
                {
                    yield return new MultiplicationToken();
                    _reader.Read();
                }
                else if (c == '/')
                {
                    yield return new DivideToken();
                    _reader.Read();
                }
                else if (c == '(')
                {
                    yield return new OpenParenthesisToken();
                    _reader.Read();
                }
                else if (c == ')')
                {
                    yield return new ClosedParenthesisToken();
                    _reader.Read();
                }
                else if (c == ',')
                {
                    yield return new CommaToken();
                    _reader.Read();
                }
                else if (char.IsLetter(c))
                {
                    var name = ParseIdentifier();
                    yield return new IdentifierToken(name);

                }

            }
        }

        private double ParseNumber()
        {
            var stringBuilder = new StringBuilder();
            var decimalExists = false;
            while (char.IsDigit((char)_reader.Peek()) || ((char)_reader.Peek() == '.'))
            {
                var digit = (char)_reader.Read();
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

        private string ParseIdentifier()
        {
            var stringBuilder = new StringBuilder();
            while (_reader.Peek() != -1 && char.IsLetterOrDigit((char)_reader.Peek()))
            {
                stringBuilder.Append((char)_reader.Read());
            }

            return stringBuilder.ToString();
        }

    }
}
