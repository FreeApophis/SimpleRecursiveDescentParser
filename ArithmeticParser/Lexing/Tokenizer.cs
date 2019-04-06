using System.Collections.Generic;
using System.IO;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Lexing
{
    public class Tokenizer
    {
        private readonly LexerRules _lexerRules;

        public Tokenizer(LexerRules lexerRules)
        {
            _lexerRules = lexerRules;
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

                foreach (var lexerRule in _lexerRules.GetRules())
                {
                    if (lexerRule.Predicate(c))
                    {
                        yield return lexerRule.CreateToken(reader);
                    }
                }
            }
        }
    }
}
