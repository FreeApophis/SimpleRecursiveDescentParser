using System;
using ArithmeticParser.Nodes;
using ArithmeticParser.Parsing;
using ArithmeticParser.Tokens;

namespace ArithmeticParser
{
    /// <summary>
    /// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
    ///
    /// Expression := [ "-" ] Term { ("+" | "-") Term }
    /// Term       := Factor { ( "*" | "/" ) Factor }
    /// Factor     := RealNumber | "(" Expression ") | Variable | Function "
    /// Function   := Identifier "(" Expression { "," Expression } ")"
    /// Variable   := Identifier
    /// Identifier := Non-digit character { Any non whitespace }
    /// RealNumber := Digit{Digit} | [Digit] "." {Digit}
    /// Digit      := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
    /// </summary>
    public class Parser : IParser
    {
        private readonly ExpressionParser _expressionParser;

        public Parser()
        {
            _expressionParser = new ExpressionParser();
        }

        public IParseNode Parse(TokenWalker walker)
        {
            return _expressionParser.Parse(walker);

        }

        public IParseNode Parse(string expression)
        {
            var tokens = new Tokenizer().Scan(expression);
            var walker = new TokenWalker(tokens);

            return Parse(walker);
        }
    }
}
