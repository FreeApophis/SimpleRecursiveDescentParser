using LambdaCalculusParser.Visitors;
using Xunit;

namespace LambdaCalculusParser.Test;

public class LambdaCalculusInterpreterTest
{
    private readonly Parser _parser = Parser.Create();

    [Fact]
    public void GivenTheIdentifyFunctionParseAndInterpretToTheIdentifyFunction()
    {
        var identityFunction = "λx.x";

        var lambdaExpression = _parser.Parse(identityFunction);

        var interpreter = new InterpreterVisitor();
        lambdaExpression.Accept(interpreter);

        var printVisitor = new NormalFormVisitor();

        interpreter.Result.AndThen(expression => expression.Accept(printVisitor));

        Assert.Equal("(λx.x)", printVisitor.Result);
    }

    [Fact(Skip = "no real interpreter")]
    public void GivenBoundAndUnboundVariableWithDifferentName()
    {
        var application = "(λx.(λy.xy))t";

        var lambdaExpression = _parser.Parse(application);

        var interpreter = new InterpreterVisitor();
        lambdaExpression.Accept(interpreter);

        var printVisitor = new NormalFormVisitor();
        interpreter.Result.AndThen(expression => expression.Accept(printVisitor));

        Assert.Equal("(λt.yt)", printVisitor.Result);
    }

    [Fact(Skip = "no real interpreter")]
    public void GivenBoundAndUnboundVariableWithTheSameName()
    {
        var application = "(λx.(λy.xy))x";

        var lambdaExpression = _parser.Parse(application);

        var interpreter = new InterpreterVisitor();
        interpreter.Result.AndThen(expression => expression.Accept(interpreter));

        var printVisitor = new NormalFormVisitor();
        lambdaExpression.Accept(printVisitor);

        Assert.Equal("(λt.yt)", printVisitor.Result);
    }

    [Fact(Skip = "no real interpreter")]
    public void GivenAnApplicationEvaluateTheApplicationCorrectly()
    {
        var application = @"(λx.(λy.x)) (λx.(λy.x))";

        var lambdaExpression = _parser.Parse(application);

        var interpreter = new InterpreterVisitor();
        var events = new ReductionEventCollector();

        ConnectEvents(interpreter, events);

        lambdaExpression.Accept(interpreter);

        var printVisitor = new NormalFormVisitor();
        interpreter.Result.AndThen(expression => expression.Accept(printVisitor));

        Assert.Collection(
            events.Events,
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
}