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

        private readonly VariableParser _variableParser;
        private readonly FunctionParser _functionParser;

        public Parser(string expression)
        {
            _expression = expression;
            _variableParser = new VariableParser();
            _functionParser = new FunctionParser(this);
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
            var result = ParseFactor();
            while (_walker.NextIsMultiplicationOrDivision())
            {
                var op = _walker.Pop();
                switch (op)
                {
                    case DivideToken _:
                        result = new DivisionOperator(result, ParseFactor());
                        break;
                    case MultiplicationToken _:
                        result = new MultiplicationOperator(result, ParseFactor());
                        break;
                }
            }

            return result;
        }

        // Factor     := RealNumber | "(" Expression ") | Variable | Function "
        private IParseNode ParseFactor()
        {
            if (_walker.NextIs<NumberToken>())
            {
                return new NumberNode(GetNumber());
            }

            if (_walker.NextIs<IdentifierToken>())
            {
                return _walker.NextIs<OpenParenthesisToken>(1) 
                    ? _functionParser.Parse(_walker) 
                    : _variableParser.Parse(_walker);
            }

            ExpectOpeningParenthesis();
            var result = ParseExpression();
            ExpectClosingParenthesis();

            return result;
        }


        private void ExpectClosingParenthesis()
        {
            if (!(_walker.NextIs<ClosedParenthesisToken>()))
            {
                throw new Exception("Expecting ')' in expression, instead got: " + (PeekNext() != null ? PeekNext().ToString() : "End of expression"));
            }
            _walker.Pop();
        }

        private void ExpectOpeningParenthesis()
        {
            if (!_walker.NextIs<OpenParenthesisToken>())
            {
                throw new Exception("Expecting Real number or '(' in expression, instead got : " + (PeekNext() != null ? PeekNext().ToString() : "End of expression"));
            }
            _walker.Pop();
        }

        private IToken PeekNext()
        {
            return _walker.Peek();
        }

        private double GetNumber()
        {
            var next = _walker.Pop();

            if (next is NumberToken nr)
            {
                return nr.Value;

            }

            throw new Exception("Expecting Real number but got " + next);
        }

    }
}
