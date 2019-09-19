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
        public void GivenASimpleProgramThenParseReturnsTheCorrectAbstractSyntaxTree()
        {
            var simpleProgram = "(λx. x) (λy. y)";

            var lambdaExpression = _parser.Parse(simpleProgram);

            Assert.IsType<Application>(lambdaExpression);
            var application = (Application)lambdaExpression;

            Assert.IsType<Term>(application.Function);
            var function = (Term)application.Function;

            Assert.Equal("x", function.Argument.Name);
            Assert.IsType<Variable>(function.Expression);
            var leftVariable = (Variable)function.Expression;
            Assert.Equal("x", leftVariable.Name);

            Assert.IsType<Term>(application.Argument);
            var argument = (Term)application.Argument;

            Assert.Equal("y", argument.Argument.Name);
            Assert.IsType<Variable>(argument.Expression);
            var rightVariable = (Variable)argument.Expression;
            Assert.Equal("y", rightVariable.Name);
        }

        [Fact]
        public void GivenAProgramAndAParsedASTWeGetTheSameProgramFromTheAST()
        {
            var testLambda = @"λs.(λz.(s z))";

            var lambdaExpression = _parser.Parse(testLambda);

            var printVisitor = new PrintVisitor();
            lambdaExpression.Accept(printVisitor);

            Assert.Equal(testLambda, printVisitor.Result);
        }

        [Fact]
        public void Experiments()
        {
            var yCobinator = @"λ f . (λ x . f (x x)) (λ x . f (x x))";

            var lambdaExpression = _parser.Parse(yCobinator);

            var graphvizVisitor = new GraphvizVisitor();
            lambdaExpression.Accept(graphvizVisitor);
        }



    }
}
