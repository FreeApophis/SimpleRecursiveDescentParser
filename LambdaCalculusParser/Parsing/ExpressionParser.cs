using System;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using Messerli.Lexer;

namespace LambdaCalculusParser.Parsing
{
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
            if (parserContext.TokenWalker.NextIs<LambdaToken>())
            {
                parserContext.TokenWalker.Consume<LambdaToken>();
                if (parserContext.TokenWalker.Pop().Token is IdentifierToken variableName)
                {
                    var variable = new Variable(variableName.Name);
                    parserContext.TokenWalker.Consume<DotToken>();
                    parserContext.BoundVariables.Push(variable);
                    var result = new Abstraction(variable, ApplicationParser.Parse(parserContext));
                    parserContext.BoundVariables.Pop();

                    return result;
                }

                throw new Exception();
            }

            if (parserContext.TokenWalker.NextIs<OpenParenthesisToken>())
            {
                parserContext.TokenWalker.Consume<OpenParenthesisToken>();
                var result = ApplicationParser.Parse(parserContext);
                parserContext.TokenWalker.Consume<ClosedParenthesisToken>();
                return result;
            }

            if (parserContext.TokenWalker.NextIs<IdentifierToken>())
            {
                var lexem = parserContext.TokenWalker.Pop();
                return new Variable(((IdentifierToken)lexem.Token).Name);
            }

            throw new Exception("EOF");
        }
    }
}