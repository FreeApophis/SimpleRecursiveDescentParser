using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
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
