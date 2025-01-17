using Funcky.Lexer.Token;

namespace ArithmeticParser.Tokens;

public class IdentifierToken(string name) : IToken
{
    public string Name { get; } = name;

    public override string ToString()
        => $"Identifier: {Name}";
}
