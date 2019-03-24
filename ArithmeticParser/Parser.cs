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
        private readonly string _expression;
        private TokenWalker _walker;
        private IParseNode _parseTree;

        private readonly FactorParser _factorParser;


        public Parser(string expression)
        {
            _expression = expression;
            _factorParser = new FactorParser(this);
        }

        public IParseNode Parse(TokenWalker walker)
        {
            throw new NotImplementedException();
        }

        public IParseNode Parse()
        {
            var tokens = new Tokenizer().Scan(_expression);
            _walker = new TokenWalker(tokens);

            _parseTree = ParseExpression();
            return _parseTree;
        }

        // Expression := [ "-" ] Term { ("+" | "-") Term }
        public IParseNode ParseExpression()
        {
            IParseNode result;
            if (_walker.NextIs<MinusToken>())
            {
                _walker.Pop();
                result = new UnaryMinusOperator(ParseTerm());
            }
            else
            {
                result = ParseTerm();
            }
            while (_walker.NextIsMinusOrPlus())
            {
                var op = _walker.Pop();
                switch (op)
                {
                    case MinusToken _:
                        result = new MinusOperator(result, ParseTerm());
                        break;
                    case PlusToken _:
                        result = new PlusOperator(result, ParseTerm());
                        break;
                }
            }
            return result;
        }

        // Term       := Factor { ( "*" | "/" ) Factor }
        private IParseNode ParseTerm()
        {
            var result = _factorParser.Parse(_walker);
            while (_walker.NextIsMultiplicationOrDivision())
            {
                var op = _walker.Pop();
                switch (op)
                {
                    case DivideToken _:
                        result = new DivisionOperator(result, _factorParser.Parse(_walker));
                        break;
                    case MultiplicationToken _:
                        result = new MultiplicationOperator(result, _factorParser.Parse(_walker));
                        break;
                }
            }

            return result;
        }
    }
}
