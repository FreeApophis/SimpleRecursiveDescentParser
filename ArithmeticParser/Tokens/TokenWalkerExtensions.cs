using System;

namespace ArithmeticParser.Tokens
{
    public static class TokenWalkerExtensions
    {
        public static void Consume(this TokenWalker walker, Type type)
        {
            var token = walker.Pop();
            if (token.GetType() != type)
            {
                throw new Exception($"Expecting {type} but got {token} ");
            }
        }

        public static bool NextIs<TType>(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.Peek(lookAhead) is TType;
        }

        public static bool NextIsMinusOrPlus(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MinusToken>(lookAhead) || walker.NextIs<PlusToken>(lookAhead);
        }

        public static bool NextIsMultiplicationOrDivision(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MultiplicationToken>(lookAhead) || walker.NextIs<DivideToken>(lookAhead);
        }
    }
}
