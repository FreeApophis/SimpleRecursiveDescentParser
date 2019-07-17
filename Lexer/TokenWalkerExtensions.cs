using System;

namespace apophis.Lexer
{
    public static class TokenWalkerExtensions
    {
        public static void Consume<TToken>(this TokenWalker walker)
        {
            var token = walker.Pop();
            if (token.GetType() is TToken)
            {
                throw new Exception($"Expecting {typeof(TToken).FullName} but got {token} ");
            }
        }

        public static bool NextIs<TType>(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.Peek(lookAhead) is TType;
        }
    }
}
