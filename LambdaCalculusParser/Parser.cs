using System.Collections.Generic;
using System.Linq;
using System.Xml;
using apophis.Lexer;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Parsing;
using LambdaCalculusParser.Tokens;

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
            _parserContext.TokenWalker.Scan(expression, lexems => lexems.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)));

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

            var lexerRules = new LexerRules();
            var tokenizer = new Tokenizer(lexerRules, s => new LexerReader(s));
            var tokenWalker = new TokenWalker(tokenizer, () => new EpsilonToken());
            return new Parser(tokenWalker, applicationParser);
        }
    }

    public class ParserContext
    {
        public ParserContext(TokenWalker tokenWalker)
        {
            TokenWalker = tokenWalker;
        }
        public TokenWalker TokenWalker { get; }
        public Stack<Variable> BoundVariables { get; } = new Stack<Variable>();

    }
}
