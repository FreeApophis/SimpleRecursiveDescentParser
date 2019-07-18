using System.Diagnostics;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Expression := [ "-" ] Term { ("+" | "-") Term }
    /// </summary>
    public class ExpressionParser : IParser
    {
        // Break the dependency cycle
        public TermParser? TermParser { get; set; }

        public IParseNode Parse(TokenWalker walker)
        {
            Debug.Assert(TermParser != null);

            IParseNode result;
            if (walker.NextIs<MinusToken>())
            {
                walker.Pop();
                result = new UnaryMinusOperator(TermParser.Parse(walker));
            } else
            {
                result = TermParser.Parse(walker);
            }
            while (walker.NextIsLineOperator())
            {
                result = walker.Pop().Token switch
                {
                    MinusToken _ => new MinusOperator(result, TermParser.Parse(walker)),
                    PlusToken _ => new PlusOperator(result, TermParser.Parse(walker)),
                    _ => result
                };
            }

            return result;
        }
    }
}
