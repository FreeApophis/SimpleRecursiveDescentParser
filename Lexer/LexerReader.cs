using Funcky.Monads;

namespace apophis.Lexer
{
    public class LexerReader : ILexerReader
    {
        private readonly string _expression;

        public LexerReader(string expression)
        {
            _expression = expression;
        }

        public int Position { get; private set; }

        public Option<char> Peek(int lookAhead = 0)
        {
            var position = Position + lookAhead;

            return position >= 0 && position < _expression.Length
                ? _expression[position]
                : Option<char>.None();
        }

        public Option<char> Read()
        {
            var result = Peek();

            Position++;

            return result;
        }
    }
}