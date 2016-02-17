using System;
using System.Collections.Generic;

namespace FormelParser
{
    /// <summary>
    /// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following Grammer in EBNF
    /// 
    /// Expression := [ "-" ] Term { ("+" | "-") Term }
    /// Term       := Factor { ( "*" | "/" ) Factor }
    /// Factor     := RealNumber | "(" Expression ")"
    /// RealNumber := Digit{Digit} | [Digit] "." {Digit}
    /// Digit      := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" 
    /// </summary>
    public class Parser
    {
        private readonly string _expression;
        private TokenWalker _walker;
        private IParseNode _parseTree;

        public Parser(string expression)
        {
            _expression = expression;
        }


        // Expression := [ "-" ] Term { ("+" | "-") Term }

        public IParseNode Parse()
        {
            var tokens = new Tokenizer().Scan(_expression);
            _walker = new TokenWalker(tokens);

            _parseTree = ParseExpression();
            return _parseTree;
        }

        public IParseNode ParseExpression()
        {
            IParseNode result;
            if (NextIsMinus())
            {
                _walker.Pop();
                result = new UnaryMinus(ParseTerm());
            }
            else
            {
                result = ParseTerm();
            }
            while (NextIsMinusOrPlus())
            {
                var op = _walker.Pop();
                if (op is MinusToken)
                {
                    result = new MinusOperator(result, ParseTerm());
                }
                if (op is PlusToken)
                {
                    result = new PlusOperator(result, ParseTerm());
                }
            }
            return result;
        }

        // Term       := Factor { ( "*" | "/" ) Factor }
        private IParseNode ParseTerm()
        {
            var result = ParseFactor();
            while (NextIsMultiplicationOrDivision())
            {
                var op = _walker.Pop();
                if (op is DivideToken)
                {
                    result = new DivisionOperator(result, ParseFactor());
                }
                if (op is MultiplicationToken)
                {
                    result = new MultiplicationOperator(result, ParseFactor());
                }
            }

            return result;
        }

        // Factor     := RealNumber | "(" Expression ")"
        private IParseNode ParseFactor()
        {
            if (NextIsDigit())
            {
                return new NumberNode(GetNumber());
            }

            ExpectOpeningParenthesis();
            var result = ParseExpression();
            ExpectClosingParenthesis();

            return result;
        }

        private void ExpectClosingParenthesis()
        {
            if (!(NextIs(typeof(ClosedParenthesisToken))))
            {
                throw new Exception("Expecting ')' in expression, instead got: " + (PeekNext() != null ? PeekNext().ToString() : "End of expression"));
            }
            _walker.Pop();
        }

        private void ExpectOpeningParenthesis()
        {
            if (!NextIsOpeningBracket())
            {
                throw new Exception("Expecting Real number or '(' in expression, instead got : " + (PeekNext() != null ? PeekNext().ToString() : "End of expression"));
            }
            _walker.Pop();
        }

        private bool NextIsMinus()
        {
            return _walker.ThereAreMoreTokens && _walker.Peek().GetType() == (typeof(MinusToken));
        }

        private bool NextIsOpeningBracket()
        {
            return NextIs(typeof(OpenParenthesisToken));
        }


        private Token PeekNext()
        {
            return _walker.ThereAreMoreTokens ? _walker.Peek() : null;
        }

        private double GetNumber()
        {
            var next = _walker.Pop();

            var nr = next as NumberToken;
            if (nr == null)
                throw new Exception("Expecting Real number but got " + next);

            return nr.Value;
        }

        private bool NextIsDigit()
        {
            if (!_walker.ThereAreMoreTokens)
                return false;
            return _walker.Peek() is NumberToken;
        }

        private bool NextIs(Type type)
        {
            return _walker.ThereAreMoreTokens && _walker.Peek().GetType() == type;
        }

        private bool NextIsMinusOrPlus()
        {
            return _walker.ThereAreMoreTokens && (NextIs(typeof(MinusToken)) || NextIs(typeof(PlusToken)));
        }

        private bool NextIsMultiplicationOrDivision()
        {
            return _walker.ThereAreMoreTokens && (NextIs(typeof(MultiplicationToken)) || NextIs(typeof(DivideToken)));
        }
    }
}
