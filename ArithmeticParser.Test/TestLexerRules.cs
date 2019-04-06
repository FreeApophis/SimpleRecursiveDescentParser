using System.Collections.Generic;
using ArithmeticParser.Lexing;

namespace ArithmeticParser.Test
{
    internal class TestLexerRules : ILexerRules
    {
        public IEnumerable<ILexerRule> GetRules()
        {
            yield return new SimpleLexerRule<EqualToken>("=");
            yield return new SimpleLexerRule<DoubleEqualToken>("==");
            yield return new SimpleLexerRule<GreaterToken>("<");
            yield return new SimpleLexerRule<GreaterEqualToken>("<=");
        }
    }
}