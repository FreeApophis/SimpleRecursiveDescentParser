using System.Linq;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
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
            Assert.IsType<GreaterEqualToken>(tokenizer.Scan("<===").First());
            Assert.IsType<DoubleEqualToken>(tokenizer.Scan("<===").Last());
        }

        [Fact]
        void GivenALexerRuleForIdentifiersDoNotReturKeyTokenInTheMiddle()
        {
            var tokenizer = CreateTestTokenizer();

            Assert.IsType<IdentifierToken>(tokenizer.Scan("sand").Single());
            Assert.IsType<IdentifierToken>(tokenizer.Scan("andor").Single());
            Assert.IsType<AndToken>(tokenizer.Scan("and").Single());
        }
    }
}
