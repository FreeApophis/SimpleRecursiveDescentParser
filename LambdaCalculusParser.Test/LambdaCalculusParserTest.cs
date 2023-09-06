﻿using Funcky.Lexer.Exceptions;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Tokens;
using LambdaCalculusParser.Visitors;
using Xunit;

namespace LambdaCalculusParser.Test;

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
        var walker = LexerRules.GetRules().Scan(@"λs.(λz.(s z))").Walker;

        Assert.IsType<LambdaToken>(walker.Pop().Token);
        Assert.IsType<IdentifierToken>(walker.Pop().Token);
        Assert.IsType<DotToken>(walker.Pop().Token);
        Assert.IsType<OpenParenthesisToken>(walker.Pop().Token);
        Assert.IsType<LambdaToken>(walker.Pop().Token);
        Assert.IsType<IdentifierToken>(walker.Pop().Token);
        Assert.IsType<DotToken>(walker.Pop().Token);
        Assert.IsType<OpenParenthesisToken>(walker.Pop().Token);
        Assert.IsType<IdentifierToken>(walker.Pop().Token);
        Assert.IsType<IdentifierToken>(walker.Pop().Token);
        Assert.IsType<ClosedParenthesisToken>(walker.Pop().Token);
        Assert.IsType<ClosedParenthesisToken>(walker.Pop().Token);
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
    public void ApplicationTest()
    {
        var application = "λm.λn.λf.λx.m f (n f x)";

        var lambdaExpression = _parser.Parse(application);

        var printVisitor = new NormalFormVisitor();
        lambdaExpression.Accept(printVisitor);

        Assert.Equal("(λm.(λn.(λf.(λx.((m f) ((n f) x))))))", printVisitor.Result);

        Assert.IsType<Abstraction>(lambdaExpression);
    }

    [Fact]
    public void FailTest()
    {
        var application = "λm.λn.λf.λx.m f (n f x";

        Assert.Throws<InvalidTokenException>(() => _parser.Parse(application));
    }
}