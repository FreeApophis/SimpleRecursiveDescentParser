﻿using System;
using apophis.Lexer.Tokens;
using Funcky.Monads;

namespace apophis.Lexer
{
    public class LexerRule : ILexerRule
    {
        public Predicate<char> Predicate { get; }
        public Func<ILexerReader, Lexem> CreateToken { get; }

        public LexerRule(Predicate<char> predicate, Func<ILexerReader, Lexem> createToken)
        {
            Predicate = predicate;
            CreateToken = createToken;
        }

        public int Weight { get; } = 0;

        public Option<Lexem> Match(ILexerReader reader)
        {
            var predicate =
                from nextCharacter in reader.Peek()
                select Predicate(nextCharacter);

            return predicate.Match(false, p => p)
                ? Option.Some(CreateToken(reader))
                : Option<Lexem>.None();
        }
    }
}