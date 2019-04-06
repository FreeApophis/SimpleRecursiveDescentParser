using System.Linq;
using ArithmeticParser.Lexing;
using Xunit;

namespace ArithmeticParser.Test
{
    public class LexerTest
    {

        Tokenizer CreateTestTokenizer()
        {
            return new Tokenizer(new TestLexerRules(), s => new LexerReader(s));
        }

        [Fact]
        void GivenSymbolsWithOverlappingPrefixesTheLexerGetsTheLongerOne()
        {
            var tokenizer = CreateTestTokenizer();

            Assert.IsType<EqualToken>(tokenizer.Scan("=").Single());
            Assert.IsType<DoubleEqualToken>(tokenizer.Scan("==").Single());
            Assert.IsType<GreaterToken>(tokenizer.Scan("<").Single());
            Assert.IsType<GreaterEqualToken>(tokenizer.Scan("<=").Single());
        }
    }
}
