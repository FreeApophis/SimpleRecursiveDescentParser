using System;
using System.Collections.Generic;
using System.Linq;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Lexing
{
    public class Tokenizer
    {
        private readonly ILexerRules _lexerRules;
        private readonly Func<string, ILexerReader> _newLexerReader;

        public Tokenizer(ILexerRules lexerRules, Func<string, ILexerReader> newLexerReader)
        {
            _lexerRules = lexerRules;
            _newLexerReader = newLexerReader;
        }

        public IEnumerable<IToken> Scan(string expression)
        {
            var reader = _newLexerReader(expression);

            while (reader.Peek().Match(false, c => true))
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
