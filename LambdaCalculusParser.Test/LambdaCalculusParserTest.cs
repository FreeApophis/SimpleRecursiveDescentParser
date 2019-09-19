using System.Collections.Generic;
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
    class ReductionEventCollector
    {
        public List<string> Events { get; } = new List<string>();

        public void OnAlphaReductionEvent(object sender, AlphaReductionEventArgs e)
        {
            Events.Add("α");
        }

        public void OnBetaReductionEvent(object sender, BetaReductionEventArgs e)
        {
            Events.Add("β");
        }

        public void OnEtaReductionEvent(object sender, EtaReductionEventArgs e)
        {
            Events.Add("η");
        }
    }

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

            Assert.IsType<Abstraction>(application.Function);
            var function = (Abstraction)application.Function;

            Assert.Equal("x", function.Argument.Name);
            Assert.IsType<Variable>(function.Expression);
            var leftVariable = (Variable)function.Expression;
            Assert.Equal("x", leftVariable.Name);

            Assert.IsType<Abstraction>(application.Argument);
            var argument = (Abstraction)application.Argument;

            Assert.Equal("y", argument.Argument.Name);
            Assert.IsType<Variable>(argument.Expression);
            var rightVariable = (Variable)argument.Expression;
            Assert.Equal("y", rightVariable.Name);
        }

        [Fact]
        public void GivenAProgramAndAParsedASTWeGetTheSameProgramFromTheAST()
        {
            var testLambda = "λs.(λz.(s z))";

            var lambdaExpression = _parser.Parse(testLambda);

            var printVisitor = new NormalFormVisitor();
            lambdaExpression.Accept(printVisitor);

            Assert.Equal("(λs.(λz.(s z)))", printVisitor.Result);
        }

        [Fact]
        public void GivenTheIdentifyFunctionParseAndInterpretToTheIdentifyFunction()
        {
            var identityFunction = "λx.x";

            var lambdaExpression = _parser.Parse(identityFunction);

            var interpreter = new InterpreterVisitor();
            lambdaExpression.Accept(interpreter);

            var printVisitor = new NormalFormVisitor();
            interpreter.Result.Accept(printVisitor);

            Assert.Equal("(λx.x)", printVisitor.Result);
        }

        [Fact]
        public void GivenAnApplicationEvaluateTheApplicationCorrectly()
        {
            var application = @"(λx.(λy.x)) (λx.(λy.x))";

            var lambdaExpression = _parser.Parse(application);

            var interpreter = new InterpreterVisitor();
            var events = new ReductionEventCollector();

            ConnectEvents(interpreter, events);

            lambdaExpression.Accept(interpreter);

            var printVisitor = new NormalFormVisitor();
            interpreter.Result.Accept(printVisitor);

            Assert.Collection(events.Events,
                e => Assert.Equal("α", e),
                e => Assert.Equal("β", e));

            Assert.Equal("λy.(λx0.(λx1.x0))", printVisitor.Result);
        }

        private static void ConnectEvents(InterpreterVisitor interpreter, ReductionEventCollector events)
        {
            interpreter.AlphaReductionEvent += events.OnAlphaReductionEvent;
            interpreter.BetaReductionEvent += events.OnBetaReductionEvent;
            interpreter.EtaReductionEvent += events.OnEtaReductionEvent;
        }

        [Fact]
        public void ApplicationTest()
        {
            var application = "λm.λn.λf.λx.m f (n f x)";

            var lambdaExpression = _parser.Parse(application);

            var printVisitor = new NormalFormVisitor();
            lambdaExpression.Accept(printVisitor);


            Assert.Equal("(λm.(λn.(λf.(λx.((m f) ((n f) x))))))", printVisitor.Result);

            Assert.IsType<Abstraction>(lambdaExpression);


        }
    }
}
