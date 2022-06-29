using System.Linq;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Parsing;
using LambdaCalculusParser.Tokens;
using Messerli.Lexer;

namespace LambdaCalculusParser
{

    /// <summary>
    /// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
    ///
    /// Application := Expression +
    /// Expression  := "λ" Identifier "." Application | "(" Application ")" | Identifier
    /// Identifier  := [a-z]*
    /// </summary>
    public class Parser
    {
        private readonly ApplicationParser _applicationParser;
        private readonly ParserContext _parserContext;

        public Parser(TokenWalker tokenWalker, ApplicationParser applicationParser)
        {
            _applicationParser = applicationParser;
            _parserContext = new ParserContext(tokenWalker);
        }

        public ILambdaExpression Parse(string expression)
        {
            _parserContext.TokenWalker.Scan(expression, lexemes => lexemes.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)));

            var result = _applicationParser.Parse(_parserContext);

            _parserContext.TokenWalker.Consume<EpsilonToken>();

            return result;
        }

        public static Parser Create()
        {
            // Create the object tree without DI Framework
            var expressionParser = new ExpressionParser();
            var applicationParser = new ApplicationParser(expressionParser);
            expressionParser.ApplicationParser = applicationParser;

            return new Parser(TokenWalker.Create<EpsilonToken>(LexerRules.GetRules()), applicationParser);
        }
    }
}
