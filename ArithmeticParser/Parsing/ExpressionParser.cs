using System;
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

        public IParseNode Parse(ITokenWalker walker) 
            => ParseNextTerm(walker, ParseFirstTerm(walker));

        private IParseNode ParseNextTerm(ITokenWalker walker, IParseNode result) 
            => walker.NextIsLineOperator()
                ? ParseNextTerm(walker, NextTerm(walker, result))
                : result;

        private IParseNode NextTerm(ITokenWalker walker, IParseNode result)
            => walker.Pop() switch
            {
                { Token: MinusToken } lexeme => new MinusOperator(result, SafeTermParser.Parse(walker), lexeme.Position),
                { Token: PlusToken } lexeme => new PlusOperator(result, SafeTermParser.Parse(walker), lexeme.Position),
                _ => result
            };

        private IParseNode ParseFirstTerm(ITokenWalker walker)
            => walker.NextIs<MinusToken>()
                ? ParseUnaryMinus(walker)
                : SafeTermParser.Parse(walker);

        private TermParser SafeTermParser 
            => TermParser ?? throw new Exception("Term Parser should be injected by the DI container, but is null");

        private IParseNode ParseUnaryMinus(ITokenWalker walker) 
            => ParseUnaryMinusTerm(walker, walker.Pop());

        private IParseNode ParseUnaryMinusTerm(ITokenWalker walker, Lexeme lexeme) 
            => new UnaryMinusOperator(SafeTermParser.Parse(walker), lexeme.Position);
    }
}
