using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public class AtomParser : IParser
    {
        private readonly TermParser _termParser;

        public AtomParser(TermParser termParser)
        {
            _termParser = termParser;
        }

        /// <summary>
        /// Parsing the following Production:
        /// Atom        := "(" Term ")"
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<OpenParenthesisToken>())
            {
                walker.Pop();
            }
            else
            {
                throw new Exception("Expecting '(' in expression, instead got: " + (walker.Peek() != null ? walker.Peek().ToString() : "End of expression"));
            }

            var result = _termParser.Parse(walker);

            if (walker.NextIs<ClosedParenthesisToken>())
            {
                walker.Pop();
            }
            else
            {
                throw new Exception("Expecting ')' in expression, instead got: " + (walker.Peek() != null ? walker.Peek().ToString() : "End of expression"));
            }

            return result;
        }
    }
}