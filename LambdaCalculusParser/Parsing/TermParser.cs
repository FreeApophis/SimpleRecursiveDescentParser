using apophis.Lexer;
using LambdaCalculusParser.Lexing;

namespace LambdaCalculusParser.Parsing
{
    public class TermParser
    {
        public IParseNode Parse(TokenWalker walker)
        {
            if (walker.Peek() is LambdaToken)
            {

            }

            return null;
            // application
        }
    }
}