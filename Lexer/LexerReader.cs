using Funcky.Monads;

namespace apophis.Lexer
{
    public class LexerReader : ILexerReader
    {
        private readonly string _expression;
        private int _position;

        public LexerReader(string expression)
        {
            _expression = expression;
            _position = 0;
        }

        public Option<char> Peek(int lookAhead = 0)
        {
            var position = _position + lookAhead;
            if (position >= 0 && position < _expression.Length)
            {
                return Option.Some(_expression[position]);
            }

            return Option<char>.None();
        }

        public Option<char> Read()
        {
            var result = Peek();

            _position++;

            return result;
        }
    }
}