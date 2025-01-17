using Funcky.Lexer.Token;

namespace ArithmeticParser.Tokens;

public class NumberToken(double value) : IToken
{
    public double Value { get; } = value;

    public override string ToString()
        => $"Number: {Value}";
}