using Funcky.Monads;

namespace apophis.Lexer
{
    public interface ILexerReader
    {
        Option<char> Peek(int lookAhead = 0);
        Option<char> Read();
    }
}