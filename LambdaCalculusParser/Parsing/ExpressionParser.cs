using Funcky.Lexer.Extensions;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser.Parsing;

public class ExpressionParser
{
    private ApplicationParser? _applicationParser;

    public ApplicationParser ApplicationParser
    {
        get => _applicationParser ?? throw new Exception("Application Parser has not been set by DI.");
        set => _applicationParser = value;
    }

    /// <summary>
    /// Parsing the following Production:
    /// </summary>
    public ILambdaExpression Parse(ParserContext parserContext)
    {
        if (parserContext.Walker.NextIs<LambdaToken>())
        {
            parserContext.Walker.Consume<LambdaToken>();
            if (parserContext.Walker.Pop().Token is IdentifierToken variableName)
            {
                var variable = new Variable(variableName.Name);
                parserContext.Walker.Consume<DotToken>();
                parserContext.BoundVariables.Push(variable);
                var result = new Abstraction(variable, ApplicationParser.Parse(parserContext));
                parserContext.BoundVariables.Pop();

                return result;
            }

            throw new Exception();
        }

        if (parserContext.Walker.NextIs<OpenParenthesisToken>())
        {
            parserContext.Walker.Consume<OpenParenthesisToken>();
            var result = ApplicationParser.Parse(parserContext);
            parserContext.Walker.Consume<ClosedParenthesisToken>();
            return result;
        }

        if (parserContext.Walker.NextIs<IdentifierToken>())
        {
            var lexeme = parserContext.Walker.Pop();
            return new Variable(((IdentifierToken)lexeme.Token).Name);
        }

        throw new Exception("EOF");
    }
}