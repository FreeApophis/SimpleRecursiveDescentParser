﻿using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

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

        private IParseNode NextToken(TokenWalker walker, IParseNode result, Lexeme lexeme)
            => lexeme.Token switch
            {
                DivideToken _ => new DivisionOperator(result, _powerTermParser.Parse(walker), lexeme.Position),
                MultiplicationToken _ => new MultiplicationOperator(result, _powerTermParser.Parse(walker), lexeme.Position),
                ModuloToken _ => new ModuloOperator(result, _powerTermParser.Parse(walker), lexeme.Position),
                _ => result
            };
    }
}
