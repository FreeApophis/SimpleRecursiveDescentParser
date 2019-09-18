using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;
using LambdaCalculusParser.Visitors;
using Xunit;

namespace LambdaCalculusParser.Test
{
    public class LambdaCalculusParserTest
    {
        private readonly Parser _parser;

        public LambdaCalculusParserTest()
        {
            _parser = Parser.Create();
        }

        [Fact]
        public void GivenLambdaCalculusSourceTheTokenizerGivesAUsefulTokenStream()
        {
            var lexerRules = new LexerRules();
            var tokenizer = new Tokenizer(lexerRules, s => new LexerReader(s));
            var tokenWalker = new TokenWalker(tokenizer, () => new EpsilonToken());

            tokenWalker.Scan(@"λs.(λz.(s z))");

            Assert.IsType<LambdaToken>(tokenWalker.Pop().Token);
            Assert.IsType<IdentifierToken>(tokenWalker.Pop().Token);
            Assert.IsType<DotToken>(tokenWalker.Pop().Token);
            Assert.IsType<OpenParenthesisToken>(tokenWalker.Pop().Token);
            Assert.IsType<LambdaToken>(tokenWalker.Pop().Token);
            Assert.IsType<IdentifierToken>(tokenWalker.Pop().Token);
            Assert.IsType<DotToken>(tokenWalker.Pop().Token);
            Assert.IsType<OpenParenthesisToken>(tokenWalker.Pop().Token);
            Assert.IsType<IdentifierToken>(tokenWalker.Pop().Token);
            Assert.IsType<WhiteSpaceToken>(tokenWalker.Pop().Token);
            Assert.IsType<IdentifierToken>(tokenWalker.Pop().Token);
            Assert.IsType<ClosedParenthesisToken>(tokenWalker.Pop().Token);
            Assert.IsType<ClosedParenthesisToken>(tokenWalker.Pop().Token);
        }

        [Fact]
        public void ParseZeroLambdaExpression()
        {
            var testLambda = @"λs.(λz.(s z))";

            var lambdaExpression = _parser.Parse(testLambda);

            var printVisitor = new PrintVisitor();
            lambdaExpression.Accept(printVisitor);

            Assert.Equal(testLambda, printVisitor.Result);
        }

        [Fact]
        public void GivenAnAstAndAPrintVisitorWeCreateCorrectSyntax()
        {

            ILambdaExpression expression = new Term(new Variable("s"), new Term(new Variable("z"), new Application(new Variable("s"), new Variable("z"))));

            var printVisitor = new PrintVisitor();
            expression.Accept(printVisitor);

            Assert.Equal("λs.(λz.(s z))", printVisitor.Result);
        }

    }
}
