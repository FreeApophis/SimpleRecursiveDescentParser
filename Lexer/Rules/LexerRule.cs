using System;
using Funcky.Monads;

namespace apophis.Lexer.Rules
{
    public class LexerRule : ILexerRule
    {
        public LexerRule(Predicate<char> predicate, Func<ILexerReader, Lexem> createToken, int weight = 0)
        {
            Predicate = predicate;
            CreateToken = createToken;
            Weight = weight;
        }

        public Predicate<char> Predicate { get; }

        public Func<ILexerReader, Lexem> CreateToken { get; }

        public int Weight { get; }

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