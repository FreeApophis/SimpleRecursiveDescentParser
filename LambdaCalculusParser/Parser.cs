using Funcky.Lexer;
using Funcky.Lexer.Extensions;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Parsing;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser;

/// <summary>
/// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
///
/// Application := Expression +
/// Expression  := "λ" Identifier "." Application | "(" Application ")" | Identifier
/// Identifier  := [a-z]*
///
/// The lambda can be either λ or \.
/// </summary>
public class Parser(LexerRuleBook ruleBook, ApplicationParser applicationParser)
{
    public ILambdaExpression Parse(string expression)
    {
        var parserContext = new ParserContext(ruleBook.Scan(expression).Walker);

        var result = applicationParser.Parse(parserContext);

        // Are we really at the end?
        parserContext.Walker.Consume<EpsilonToken>();

        return result;
    }

    public static Parser Create()
    {
        // Create the object tree without DI Framework
        var expressionParser = new ExpressionParser();
        var applicationParser = new ApplicationParser(expressionParser);
        expressionParser.ApplicationParser = applicationParser;

        return new Parser(LexerRules.GetRules(), applicationParser);
    }
}