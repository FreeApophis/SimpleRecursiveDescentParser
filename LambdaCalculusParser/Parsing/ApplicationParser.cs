using Funcky.Lexer.Extensions;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Parsing;

public class ApplicationParser(ExpressionParser expressionParser)
{
    /// <summary>
    /// Parsing the following Production:
    /// Application := Expression +
    /// </summary>
    public ILambdaExpression Parse(ParserContext parserContext)
    {
        var result = expressionParser.Parse(parserContext);
        while (!parserContext.Walker.NextIs<ClosedParenthesisToken>() &&
               !parserContext.Walker.NextIs<EpsilonToken>())
        {
            result = new Application(result, expressionParser.Parse(parserContext));
        }

        return result;
    }
}