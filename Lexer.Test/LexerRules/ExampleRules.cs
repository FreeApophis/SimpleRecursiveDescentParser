using System.Collections.Generic;
using System.Text;
using apophis.Lexer.Rules;
using apophis.Lexer.Test.Tokens;

namespace apophis.Lexer.Test.LexerRules
{
    internal class ExampleRules : ILexerRules
    {
        public IEnumerable<ILexerRule> GetRules()
        {
            yield return new SimpleLexerRule<EqualToken>("=");
            yield return new SimpleLexerRule<DoubleEqualToken>("==");
            yield return new SimpleLexerRule<GreaterToken>("<");
            yield return new SimpleLexerRule<GreaterEqualToken>("<=");
            yield return new SimpleLexerRule<AndToken>("and");
            yield return new SimpleLexerRule<SpaceToken>(" ");
            yield return new LexerRule(char.IsLetter, ScanIdentifier);
        }

        private static Lexem ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();
            while (reader.Peek().Match(false, char.IsLetterOrDigit))
            {
                stringBuilder.Append(reader.Read().Match(' ', c => c));
            }

            return new Lexem(new IdentifierToken(stringBuilder.ToString()), new Position(startPosition, reader.Position - startPosition));
        }
    }
}