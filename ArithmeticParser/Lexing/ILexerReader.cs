using Funcky.Monads;

namespace ArithmeticParser.Lexing
{
    public interface ILexerReader
    {
        Option<char> Peek(int lookAhead = 0);
        Option<char> Read();
    }
}