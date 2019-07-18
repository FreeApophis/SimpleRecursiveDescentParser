using System.Linq;
using apophis.Lexer;
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

            Assert.IsType<EqualToken>(tokenizer.Scan("=").Single().Token);
            Assert.IsType<DoubleEqualToken>(tokenizer.Scan("==").Single().Token);
            Assert.IsType<GreaterToken>(tokenizer.Scan("<").Single().Token);
            Assert.IsType<GreaterEqualToken>(tokenizer.Scan("<=").Single().Token);
            Assert.IsType<GreaterEqualToken>(tokenizer.Scan("<===").First().Token);
            Assert.IsType<DoubleEqualToken>(tokenizer.Scan("<===").Last().Token);
        }

        [Fact]
        void GivenALexerRuleForIdentifiersDoNotReturKeyTokenInTheMiddle()
        {
            var tokenizer = CreateTestTokenizer();

            Assert.IsType<IdentifierToken>(tokenizer.Scan("sand").Single().Token);
            Assert.IsType<IdentifierToken>(tokenizer.Scan("andor").Single().Token);
            Assert.IsType<AndToken>(tokenizer.Scan("and").Single().Token);
        }

        [Fact]
        void GivenLexerRulesTheLexemsHaveTheRightPositions()
        {
            var tokenizer = CreateTestTokenizer();

            var tokens = tokenizer.Scan("love and sand and testing").ToList();
            Assert.Equal(9, tokens.Count);

            Assert.IsType<IdentifierToken>(tokens[0].Token);

            Assert.Equal(0, tokens[0].Position.StartPosition);
            Assert.Equal(4, tokens[0].Position.Length);
            Assert.Equal(4, tokens[0].Position.EndPosition);

            Assert.IsType<SpaceToken>(tokens[3].Token);

            Assert.Equal(8, tokens[3].Position.StartPosition);
            Assert.Equal(1, tokens[3].Position.Length);
            Assert.Equal(9, tokens[3].Position.EndPosition);

            Assert.IsType<AndToken>(tokens[6].Token);

            Assert.Equal(14, tokens[6].Position.StartPosition);
            Assert.Equal(3, tokens[6].Position.Length);
            Assert.Equal(17, tokens[6].Position.EndPosition);

        }
    }
}
