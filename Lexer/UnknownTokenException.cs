using System;
using Funcky.Monads;

namespace apophis.Lexer
{
    public class UnknownTokenException : Exception
    {
        private readonly Option<char> _token;
        private readonly int _position;

        public UnknownTokenException(Option<char> token, int position)
        {
            _token = token;
            _position = position;
        }

        public override string ToString()
        {
            return $"Unknown Token '{_token}' on position: {_position}";
        }
    }
}