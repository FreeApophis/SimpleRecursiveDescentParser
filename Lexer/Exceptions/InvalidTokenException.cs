using System;

namespace apophis.Lexer.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) :
            base(message)
        {
        }
    }
}
