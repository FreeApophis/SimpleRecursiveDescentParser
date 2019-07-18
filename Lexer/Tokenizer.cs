using System;
using System.Collections.Generic;
using System.Linq;
using apophis.Lexer.Tokens;
using Funcky.Monads;

namespace apophis.Lexer
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

        public IEnumerable<Lexem> Scan(string expression)
        {
            var reader = _newLexerReader(expression);

            while (reader.Peek().Match(false, c => true))
            {
                yield return SelectLexerRule(reader)
                    .Match(
                        none: () => throw new UnknownTokenException(),
                        some: t => t
                        );
            }
        }

        private Option<Lexem> SelectLexerRule(ILexerReader reader)
        {
            return _lexerRules
                .GetRules()
                .OrderByDescending(rule => rule.Weight)
                .Select(rule => rule.Match(reader))
                .First(mt => mt.Match(none: false, some: t => true));
        }
    }
}
