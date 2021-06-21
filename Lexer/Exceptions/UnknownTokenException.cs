using System;
using Funcky.Monads;

namespace apophis.Lexer.Exceptions
{
    public class UnknownTokenException : Exception
    {
        private readonly Option<char> _token;
        private readonly LinePosition _position;

        public UnknownTokenException(Option<char> token, LinePosition position)
            : base($"Unknown Token '{token.Match(none: 'Ɛ', some: t => t)}' at Line {position.Line} Column {position.Column}")
        {
            _token = token;
            _position = position;
        }
    }
}