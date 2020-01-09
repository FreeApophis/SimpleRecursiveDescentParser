using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
    /// <summary>
    /// A lexem represents a string token and it's associated position.
    /// </summary>
    public class Lexem
    {
        public Lexem(IToken token, Position position)
        {
            Token = token;
            Position = position;
        }

        public IToken Token { get; }

        public Position Position { get; }
    }
}
