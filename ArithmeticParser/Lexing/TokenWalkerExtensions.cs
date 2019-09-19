using System;
using apophis.Lexer;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Lexing
{
    public static class TokenWalkerExtensions
    {
        public static bool NextIsLineOperator(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MinusToken>(lookAhead) || walker.NextIs<PlusToken>(lookAhead);
        }

        public static bool NextIsDotOperator(this TokenWalker walker, int lookAhead = 0)
        {
            return walker.NextIs<MultiplicationToken>(lookAhead) || walker.NextIs<DivideToken>(lookAhead) || walker.NextIs<ModuloToken>(lookAhead);
        }

        public static void ExpectClosingParenthesis(this TokenWalker walker)
        {
            if (!(walker.NextIs<ClosedParenthesisToken>()))
            {
                throw new Exception("Expecting ')' in expression, instead got: " + (walker.Peek() != null ? walker.Peek().Token.ToString() : "End of expression"));
            }
            walker.Pop();
        }

        public static void ExpectOpeningParenthesis(this TokenWalker walker)
        {
            if (!walker.NextIs<OpenParenthesisToken>())
            {
                throw new Exception("Expecting Real number or '(' in expression, instead got : " + (walker.Peek() != null ? walker.Peek().Token.ToString() : "End of expression"));
            }
            walker.Pop();
        }
    }
}
