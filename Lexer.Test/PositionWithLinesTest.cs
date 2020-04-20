using System.Linq;
using apophis.Lexer.Test.LexerRules;
using Xunit;

namespace apophis.Lexer.Test
{
    public class PositionWithLinesTest
    {
        private const string _exampleTextWihtNewLines = "Hello\r\n\r\nThis is a test\nWe are on line four\nLine five\r\nthe end";

        [Fact]
        public void GiveALexerAndALineSeperatorThePositionsAreGivenInLineAndColumn()
        {
            var tokenizer = new Tokenizer(new WordTokenizerWithLines(), s => new LexerReader(s), lexems => new LinePositionCalculator(lexems));

            var lexems = tokenizer.Scan(_exampleTextWihtNewLines);
            
            var positions = new LinePositionCalculator(lexems);

            // hello on line 1
            Assert.Equal(1, positions.CalculateLinePosition(lexems[0]).Line);
            Assert.Equal(1, positions.CalculateLinePosition(lexems[0]).Column);
            Assert.Equal(5, positions.CalculateLinePosition(lexems[0]).Length);

            // This on line 3
            Assert.Equal(3, positions.CalculateLinePosition(lexems[3]).Line);
            Assert.Equal(1, positions.CalculateLinePosition(lexems[3]).Column);
            Assert.Equal(4, positions.CalculateLinePosition(lexems[3]).Length);

            // is on line 3
            Assert.Equal(3, positions.CalculateLinePosition(lexems[5]).Line);
            Assert.Equal(6, positions.CalculateLinePosition(lexems[5]).Column);
            Assert.Equal(2, positions.CalculateLinePosition(lexems[5]).Length);

            // end at the last line of the file
            Assert.Equal(6, positions.CalculateLinePosition(lexems[27]).Line);
            Assert.Equal(5, positions.CalculateLinePosition(lexems[27]).Column);
            Assert.Equal(3, positions.CalculateLinePosition(lexems[27]).Length);

        }
    }
}
