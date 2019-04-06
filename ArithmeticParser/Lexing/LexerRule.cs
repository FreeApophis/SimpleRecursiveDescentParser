using System;
using System.IO;
using ArithmeticParser.Tokens;
using Funcky.Monads;

namespace ArithmeticParser.Lexing
{
    public class LexerRule : ILexerRule
    {
        public Predicate<char> Predicate { get; }
        public Func<TextReader, IToken> CreateToken { get; }

        public LexerRule(Predicate<char> predicate, Func<TextReader, IToken> createToken)
        {
            Predicate = predicate;
            CreateToken = createToken;
        }

        public int Weight { get; } = 0;

        public Option<IToken> Match(TextReader reader)
        {
            var c = (char)reader.Peek();

            return Predicate(c)
                ? Option.Some(CreateToken(reader))
                : Option<IToken>.None();
        }
    }
}