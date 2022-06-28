using System;
using System.Diagnostics;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Expression := [ "-" ] Term { ("+" | "-") Term }
    /// </summary>
    public class ExpressionParser
    {
        // Break the dependency cycle
        public TermParser? TermParser { get; set; }

        public IParseNode Parse(TokenWalker walker)
        {
            Debug.Assert(TermParser != null);

            IParseNode result = walker.NextIs<MinusToken>()
                ? ParseUnaryMinus(walker)
                : SafeTermParser.Parse(walker);

            while (walker.NextIsLineOperator())
            {
                var lexem = walker.Pop();
                result = lexem.Token switch
                {
                    MinusToken _ => new MinusOperator(result, SafeTermParser.Parse(walker), lexem.Position),
                    PlusToken _ => new PlusOperator(result, SafeTermParser.Parse(walker), lexem.Position),
                    _ => result
                };
            }

            return result;
        }

        private TermParser SafeTermParser 
            => TermParser ?? throw new NotImplementedException();

        private IParseNode ParseUnaryMinus(TokenWalker walker)
        {
            var lexem = walker.Pop();

            return new UnaryMinusOperator(SafeTermParser.Parse(walker), lexem.Position);
        }
    }
}
