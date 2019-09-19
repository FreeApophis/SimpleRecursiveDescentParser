using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Parsing
{
    public class ApplicationParser : IParser
    {
        private readonly AtomParser _atomParser;

        public ApplicationParser(AtomParser atomParser)
        {
            _atomParser = atomParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Application := Atom Application | Ɛ
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            var result = _atomParser.Parse(walker);

            while (walker.NextIs<IdentifierToken>() || walker.NextIs<OpenParenthesisToken>())
            {
                result = new Application(result, _atomParser.Parse(walker));
            }

            return result;
        }
    }
}
