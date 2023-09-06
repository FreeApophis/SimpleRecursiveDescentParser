using ArithmeticParser.Tokens;
using Funcky.Lexer;
using Funcky.Lexer.Extensions;

namespace ArithmeticParser.Lexing
{
    public static class TokenWalkerExtensions
    {
        public static bool NextIsLineOperator(this ILexemeWalker walker, int lookAhead = 0)
            => walker.Peek(lookAhead) is { Token: MinusToken or PlusToken };

        public static bool NextIsDotOperator(this ILexemeWalker walker, int lookAhead = 0)
            => walker.Peek(lookAhead).Token is MultiplicationToken or DivideToken or ModuloToken;

        public static Lexeme ExpectClosingParenthesis(this ILexemeWalker walker) 
            => walker.NextIs<ClosedParenthesisToken>()
                ? walker.Pop()
                : throw new Exception($"Expecting ')' in expression, instead got: {walker.Peek().Token}");

        public static Lexeme ExpectOpeningParenthesis(this ILexemeWalker walker) 
            => walker.NextIs<OpenParenthesisToken>()
                ? walker.Pop()
                : throw new Exception($"Expecting Real number or '(' in expression, instead got: {walker.Peek().Token}");
    }
}
