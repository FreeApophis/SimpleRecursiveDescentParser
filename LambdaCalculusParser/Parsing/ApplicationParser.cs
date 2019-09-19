using apophis.Lexer;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Parsing
{
    public class ApplicationParser
    {
        private readonly ExpressionParser _expressionParser;

        public ApplicationParser(ExpressionParser expressionParser)
        {
            _expressionParser = expressionParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Application := Expression +
        /// </summary>
        public ILambdaExpression Parse(ParserContext parserContext)
        {
            var result = _expressionParser.Parse(parserContext);
            while (!parserContext.TokenWalker.NextIs<ClosedParenthesisToken>() &&
                   !parserContext.TokenWalker.NextIs<EpsilonToken>())
            {
                result = new Application(result, _expressionParser.Parse(parserContext));
            }

            return result;
        }
    }
}
