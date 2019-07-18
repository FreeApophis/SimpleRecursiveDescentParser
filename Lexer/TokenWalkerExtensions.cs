using System;

namespace apophis.Lexer
{
    public static class TokenWalkerExtensions
    {
        public static Lexem Consume<TToken>(this TokenWalker walker)
        {
            var lexem = walker.Pop();

            if (lexem.Token.GetType() is TToken)
            {
                throw new Exception($"Expecting {typeof(TToken).FullName} but got {lexem.Token} ");
            }

            return lexem;
        }

        public static bool NextIs<TType>(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.Peek(lookAhead).Token is TType;
        }
    }
}
