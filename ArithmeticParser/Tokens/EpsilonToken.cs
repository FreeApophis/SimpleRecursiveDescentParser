using Funcky.Lexer.Token;

namespace ArithmeticParser.Tokens;

/// <summary>
/// The epsilon token signifies the end of the input.
/// </summary>
public class EpsilonToken : IEpsilonToken
{
    public override string ToString() => "ε";
}