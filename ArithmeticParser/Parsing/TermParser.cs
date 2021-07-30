using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Term       := Factor { ( "*" | "/" ) Factor }
    /// </summary>
    public class TermParser
    {
        private readonly PowerTermParser _powerTermParser;

        public TermParser(PowerTermParser powerTermParser)
            => _powerTermParser = powerTermParser;

        /// <summary>
        /// Overloaded Parse function to parse a Term
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
            => ParseNextDot(walker, _powerTermParser.Parse(walker));

        private IParseNode ParseNextDot(TokenWalker walker, IParseNode result)
            => walker.NextIsDotOperator()
                ? ParseNextDot(walker, NextToken(walker, result, walker.Pop()))
                : result;

        private IParseNode NextToken(TokenWalker walker, IParseNode result, Lexem lexem)
            => lexem.Token switch
            {
                DivideToken _ => new DivisionOperator(result, _powerTermParser.Parse(walker), lexem.Position),
                MultiplicationToken _ => new MultiplicationOperator(result, _powerTermParser.Parse(walker), lexem.Position),
                ModuloToken _ => new ModuloOperator(result, _powerTermParser.Parse(walker), lexem.Position),
                _ => result
            };
    }
}
