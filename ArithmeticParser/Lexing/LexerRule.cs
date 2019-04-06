using System;
using System.IO;
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
}