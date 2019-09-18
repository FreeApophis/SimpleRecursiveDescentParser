using System.Collections.Generic;
using System.Text;
using apophis.Lexer;
using apophis.Lexer.Rules;
using ArithmeticParser.Lexing;

namespace LambdaCalculusParser.Lexing
{
    /// <summary>
    /// This class represents the necessary lexer rules for the arithmetic parser
    /// </summary>
    public class LexerRules : ILexerRules
    {
        /// <inheritdoc />
        public IEnumerable<ILexerRule> GetRules()
        {
            //yield return new LexerRule(IsLambda, () => new LambdaToken());
            //yield return new LexerRule(c => char.IsDigit(c) || c == '.', ScanNumber);
            yield return new SimpleLexerRule<OpenParenthesisToken>("(");
            yield return new SimpleLexerRule<ClosedParenthesisToken>(")");
        }

        private static Lexem ScanIdentifier(ILexerReader reader)
        {
            var startPosition = reader.Position;
            var stringBuilder = new StringBuilder();

            return new Lexem(null, new Position(0,0));
        }
    }
}
