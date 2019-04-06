using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(expression)))
            using (var reader = new StreamReader(stream))
            {
                // enumerate here, otherwise the streams are disposed before we enumerated the tokens
                return ScanLoop(reader).ToList();
            }
        }

        private IEnumerable<IToken> ScanLoop(StreamReader reader)
        {
            while (reader.Peek() != -1)
            {
                foreach (var lexerRule in _lexerRules.GetRules().OrderByDescending(rule => rule.Weight))
                {
                    var token = lexerRule
                        .Match(reader)
                        .Match(() => null as IToken, t => t);

                    if (token != null && token.GetType() != typeof(WhiteSpaceToken))
                    {
                        yield return token;
                    }
                }
            }
        }
    }
}
