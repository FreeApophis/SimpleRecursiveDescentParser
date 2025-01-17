using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Parsing;
using Funcky;
using Funcky.Lexer;

namespace ArithmeticParser;

/// <summary>
/// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
///
/// Expression := [ "-" ] Term { ("+" | "-") Term }
/// Term       := PowerTerm { ( "*" | "/" ) PowerTerm }
/// PowerTerm  := Factor { "^" Factor }
/// Factor     := RealNumber | "(" Expression ") | Variable | Function "
/// Function   := Identifier "(" Expression { "," Expression } ")"
/// Variable   := Identifier
/// Identifier := Non-digit character { Any non whitespace }
/// RealNumber := Digit{Digit} | [Digit] "." {Digit}
/// Digit      := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
/// </summary>
public class Parser(LexerRuleBook ruleBook, ExpressionParser expressionParser)
{
    public static Parser Create()
    {
        // Create the object tree without DI Framework
        var expressionParser = new ExpressionParser();
        var variableParser = new VariableParser();
        var functionParser = new FunctionParser(expressionParser);
        var factorParser = new FactorParser(expressionParser, variableParser, functionParser);
        var powerTermParser = new PowerTermParser(factorParser);
        var termParser = new TermParser(powerTermParser);
        expressionParser.TermParser = termParser;

        return new Parser(LexerRules.GetRules(), expressionParser);
    }

    public IParseNode Parse(string expression)
        => expressionParser.Parse(ruleBook.Scan(expression).Walker);

}