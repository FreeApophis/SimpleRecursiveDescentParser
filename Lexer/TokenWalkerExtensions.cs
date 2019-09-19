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
                _ => throw new Exception($"Expecting {typeof(TToken).FullName} but got {lexem.Token} ")
            };
        }

        public static bool NextIs<TType>(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.Peek(lookAhead).Token is TType;
        }
    }
}
