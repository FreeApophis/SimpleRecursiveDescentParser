using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Term       := Factor { ( "*" | "/" ) Factor }
    /// </summary>
    public class TermParser : IParser
    {
        private readonly PowerTermParser _powerTermParser;

        public TermParser(PowerTermParser powerTermParser)
        {
            _powerTermParser = powerTermParser;
        }

        /// <summary>
        /// Overloaded Parse function to parse a Term
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
        {
            var result = _powerTermParser.Parse(walker);
            while (walker.NextIsDotOperator())
            {
                var op = walker.Pop();
                switch (op)
                {
                    case DivideToken _:
                        result = new DivisionOperator(result, _powerTermParser.Parse(walker));
                        break;
                    case MultiplicationToken _:
                        result = new MultiplicationOperator(result, _powerTermParser.Parse(walker));
                        break;
                    case ModuloToken _:
                        result = new ModuloOperator(result, _powerTermParser.Parse(walker));
                        break;
                }
            }

            return result;
        }
    }
}
