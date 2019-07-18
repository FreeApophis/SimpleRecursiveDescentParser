using System.Linq;
using apophis.Lexer.Test.LexerRules;
using apophis.Lexer.Test.Tokens;
using Xunit;

namespace apophis.Lexer.Test
{
    /// <summary>
    /// Test to verify the functionality of the lexer
    /// </summary>
    public class LexerTest
    {
        Tokenizer CreateTestTokenizer()
        {
            return new Tokenizer(new ExampleRules(), s => new LexerReader(s));
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

            var lexems = tokenizer.Scan("love and sand and testing").ToList();
            Assert.Equal(9, lexems.Count);

            //Assert.IsType<IdentifierToken>(lexems[0].Token);

            Assert.Equal(0, lexems[0].Position.StartPosition);
            Assert.Equal(4, lexems[0].Position.Length);
            Assert.Equal(4, lexems[0].Position.EndPosition);

            Assert.IsType<SpaceToken>(lexems[3].Token);

            Assert.Equal(8, lexems[3].Position.StartPosition);
            Assert.Equal(1, lexems[3].Position.Length);
            Assert.Equal(9, lexems[3].Position.EndPosition);

            Assert.IsType<AndToken>(lexems[6].Token);

            Assert.Equal(14, lexems[6].Position.StartPosition);
            Assert.Equal(3, lexems[6].Position.Length);
            Assert.Equal(17, lexems[6].Position.EndPosition);
        }

        [Fact]
        void GivenALexerMissingAProductionForAGivenStringItShouldThrowAnException()
        {
            var tokenizer = new Tokenizer(new EmptyRules(), s => new LexerReader(s));

            var lexems = tokenizer.Scan("You can't tokenize this!");

            Assert.Throws<UnknownTokenException>(() => lexems.Count());
        }
    }
}
