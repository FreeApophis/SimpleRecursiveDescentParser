using System;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Term       := Factor { ( "*" | "/" ) Factor }
    /// </summary>
    class TermParser : IParser
    {
        private readonly FactorParser _factorParser;

        public TermParser(Parser parser)
        {
            _factorParser = new FactorParser(parser);
        }

        /// <summary>
        /// Overloaded Parse function to parse a Term
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
        {
            var result = _factorParser.Parse(walker);
            while (walker.NextIsMultiplicationOrDivision())
            {
                var op = walker.Pop();
                switch (op)
                {
                    case DivideToken _:
                        result = new DivisionOperator(result, _factorParser.Parse(walker));
                        break;
                    case MultiplicationToken _:
                        result = new MultiplicationOperator(result, _factorParser.Parse(walker));
                        break;
                }
            }

            return result;
        }
    }
}
