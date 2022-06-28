using System;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Lexing
{
    public static class TokenWalkerExtensions
    {
        public static bool NextIsLineOperator(this ITokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MinusToken>(lookAhead) || walker.NextIs<PlusToken>(lookAhead);
        }

        public static bool NextIsDotOperator(this ITokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MultiplicationToken>(lookAhead) || walker.NextIs<DivideToken>(lookAhead) || walker.NextIs<ModuloToken>(lookAhead);
        }

        public static void ExpectClosingParenthesis(this ITokenWalker walker)
        {
            if (!(walker.NextIs<ClosedParenthesisToken>()))
            {
                throw new Exception($"Expecting ')' in expression, instead got: {walker.Peek().Token}");
            }
            walker.Pop();
        }

        public static void ExpectOpeningParenthesis(this ITokenWalker walker)
        {
            if (!walker.NextIs<OpenParenthesisToken>())
            {
                throw new Exception($"Expecting Real number or '(' in expression, instead got: {walker.Peek().Token}");
            }
            walker.Pop();
        }
    }
}
