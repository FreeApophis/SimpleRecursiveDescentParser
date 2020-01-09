using System;
using System.Collections.Generic;
using System.Linq;
using apophis.Lexer.Rules;
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

        public List<Lexem> Scan(string expression)
        {
            var reader = _newLexerReader(expression);

            var lexems = new List<Lexem>();
            while (reader.Peek().Match(false, c => true))
            {
                var lexem = SelectLexerRule(reader, lexems)
                    .Match(
                        none: () => throw new UnknownTokenException(reader.Peek(), reader.Position),
                        some: t => t);

                lexems.Add(lexem);
            }

            return lexems;
        }

        private Option<Lexem> SelectLexerRule(ILexerReader reader, List<Lexem> context)
        {
            return _lexerRules
                .GetRules()
                .Where(rule => rule.IsActive(context))
                .OrderByDescending(rule => rule.Weight)
                .Select(rule => rule.Match(reader))
                .FirstOrDefault(mt => mt.Match(none: false, some: t => true));
        }
    }
}
