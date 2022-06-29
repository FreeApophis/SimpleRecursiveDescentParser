using System;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Lexing
{
    public static class TokenWalkerExtensions
    {
        public static bool NextIsLineOperator(this ITokenWalker walker, int lookAhead = 0)
            => walker.Peek(lookAhead).Token is MinusToken or PlusToken;

        public static bool NextIsDotOperator(this ITokenWalker walker, int lookAhead = 0)
            => walker.Peek(lookAhead).Token is MultiplicationToken or DivideToken or ModuloToken;

        public static Lexeme ExpectClosingParenthesis(this ITokenWalker walker) 
            => walker.NextIs<ClosedParenthesisToken>()
                ? walker.Pop()
                : throw new Exception($"Expecting ')' in expression, instead got: {walker.Peek().Token}");

        public static Lexeme ExpectOpeningParenthesis(this ITokenWalker walker) 
            => walker.NextIs<OpenParenthesisToken>()
                ? walker.Pop()
                : throw new Exception($"Expecting Real number or '(' in expression, instead got: {walker.Peek().Token}");
    }
}
