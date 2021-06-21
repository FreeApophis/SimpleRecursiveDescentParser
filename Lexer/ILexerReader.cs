using Funcky.Monads;
using System.Collections.Generic;

namespace apophis.Lexer
{
    public interface ILexerReader
    {
        int Position { get; }

        Option<char> Peek(int lookAhead = 0);

        Option<char> Read();
    }
}