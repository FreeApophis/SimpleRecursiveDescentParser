using ArithmeticParser.Tokens;
using Funcky.Lexer;
using static Funcky.Functional;

namespace ArithmeticParser.Lexing
{
    /// <summary>
    /// This class represents the necessary lexer rules for the arithmetic parser
    /// </summary>
    public static class LexerRules
    {
        public static LexerRuleBook GetRules()
            => LexerRuleBook.Builder
                .AddRule(char.IsWhiteSpace, ScanWhiteSpace)
                .AddRule(c => char.IsDigit(c) || c == '.', ScanNumber)
                .AddRule(char.IsLetter, ScanIdentifier)
                .AddSimpleRule<MinusToken>("-")
                .AddSimpleRule<PlusToken>("+")
                .AddSimpleRule<MultiplicationToken>("*")
                .AddSimpleRule<DivideToken>("/")
                .AddSimpleRule<ModuloToken>("%")
                .AddSimpleRule<PowerToken>("^")
                .AddSimpleRule<OpenParenthesisToken>("(")
                .AddSimpleRule<ClosedParenthesisToken>(")")
                .AddSimpleRule<CommaToken>(",")
                .WithPostProcess(lexemes => lexemes.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)))
                .WithEpsilonToken<EpsilonToken>()
                .Build();

        private static Lexeme ScanWhiteSpace(ILexemeBuilder builder)
        {
            while (builder.Peek().Match(none: false, some: char.IsWhiteSpace))
            {
                // we are not interested in what kind of whitespace, so we just discard the result
                builder.Discard();
            }

            return builder.Build(new WhiteSpaceToken());
        }

        private static Lexeme ScanNumber(ILexemeBuilder builder)
        {
            var decimalExists = false;
            while (builder.Peek().Match(none: false, some: char.IsDigit) || builder.Peek().Match(none: false, some: c => c == '.'))
            {
                var digit = builder.Peek();
                var isDot =
                    from d in digit
                    select d is '.';

                if (isDot.Match(none: false, some: Identity))
                {
                    if (decimalExists)
                    {
                        throw new Exception("Multiple dots in decimal number");
                    }
                    decimalExists = true;
                }

                builder.Retain();
            }

            return double.TryParse(builder.CurrentToken, out var number)
                ? builder.Build(new NumberToken(number))
                : throw new Exception("Could not parse number: " + builder.CurrentToken);
        }

        private static Lexeme ScanIdentifier(ILexemeBuilder builder)
        {
            while (builder.Peek().Match(false, char.IsLetterOrDigit))
            {
                builder.Retain();
            }

            return builder.Build(new IdentifierToken(builder.CurrentToken));
        }
    }
}
