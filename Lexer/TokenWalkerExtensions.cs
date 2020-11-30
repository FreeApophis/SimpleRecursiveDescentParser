using System;
using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
    public static class TokenWalkerExtensions
    {
        public static Lexem Consume<TToken>(this TokenWalker walker)
            where TToken : IToken
        {
            var lexem = walker.Pop();

            return lexem.Token switch
            {
                TToken _ => lexem,
                _ => HandleMissingLexem<TToken>(lexem, walker),
            };
        }

        private static Lexem HandleMissingLexem<TToken>(Lexem lexem, TokenWalker walker)
        {
            var position = walker.CalculateLinePosition(lexem);

            throw new Exception($"Expecting {typeof(TToken).FullName} but got {lexem.Token} at Line {position.Line} Column {position.Column}.");
        }

        public static bool NextIs<TType>(this TokenWalker walker, int lookAhead = 0)
            => walker.Peek(lookAhead).Token is TType;
    }
}
