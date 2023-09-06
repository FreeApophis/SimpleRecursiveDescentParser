using ArithmeticParser.Nodes;
using ArithmeticParser.Visitors;
using Autofac;
using Xunit;

namespace ArithmeticParser.Test
{
    public class ArithmeticParserTest
    {
        private readonly Parser _parser;

        public ArithmeticParserTest()
        {
            _parser = new CompositionRoot()
                .Build()
                .Resolve<Parser>();
        }

        [Fact]
        public void ParseSimpleArithmeticExpressionCorrectly()
        {
            var parseTree = _parser.Parse("5+17*22");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(379.0, calculator.Result);
        }

        [Fact]
        public void ParseSingleNumberCorrectly()
        {
            var parseTree = _parser.Parse("22");

            var number = parseTree as NumberNode;
            Assert.NotNull(number);
            Assert.Equal(22.0, number?.Number);

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(22.0, calculator.Result);

            var parenthesisVisitor = new FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("22", parenthesisVisitor.Result);
        }

        [Fact]
        public void ParseSimpleFloatCorrectly()
        {
            var parseTree = _parser.Parse("3.14159265");

            var number = parseTree as NumberNode;
            Assert.NotNull(number);
            Assert.Equal(3.14159265, number?.Number);

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(3.14159265, calculator.Result);

            var parenthesisVisitor = new FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("3.14159265", parenthesisVisitor.Result);
        }

        [Fact]
        public void ParseSimpleAdditionCorrectly()
        {
            var parseTree = _parser.Parse("1+2+3+4+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(210.0, calculator.Result);
        }

        [Fact]
        public void ParseSimpleSubtractionCorrectly()
        {
            var parseTree = _parser.Parse("100-1-2-3-4-5-6-7-8-9)");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(55.0, calculator.Result);

        }

        [Fact]
        public void ParseSimpleMultiplicationCorrectly()
        {
            var parseTree = _parser.Parse("1*2*3*4*5*6*7");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(5040.0, calculator.Result);
        }

        [Fact]
        public void ParseSimpleDivisionCorrectly()
        {
            var parseTree = _parser.Parse("(1/2)/(1/10)/2");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(2.5, calculator.Result);
        }

        [Fact]
        public void ParseComplexArithmeticExampleCorrectly()
        {
            var parseTree = _parser.Parse("145.2/3+7-(8*45+22*(2-19))-88/8 + 17");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(75.4, calculator.Result);

            var parenthesisVisitor = new FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("(((((145.2/3)+7)-((8*45)+(22*(2-19))))-(88/8))+17)", parenthesisVisitor.Result);
        }

        [Fact]
        public void CreatesArithmeticExpressionWithMinimalAmountOfParenthesis()
        {
            var plusParseTree = _parser.Parse("(1+(2+(3+4)))");

            var parenthesisVisitor = new MinimalParenthesisVisitor();
            plusParseTree.Accept(parenthesisVisitor);

            Assert.Equal("1+2+3+4", parenthesisVisitor.Result);

            var minusParseTree = _parser.Parse("(-1-(2-(3-4)))");

            var minimalParenthesisVisitor = new MinimalParenthesisVisitor();
            minusParseTree.Accept(minimalParenthesisVisitor);

            Assert.Equal("-1-(2-(3-4))", minimalParenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreateMultiplicationWithMinimalAmountOfParenthesis()
        {
            var parseTree = _parser.Parse("6*(1+2+3)");

            var parenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("6*(1+2+3)", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreatesDivisionWithMinimalAmountOfParenthesis()
        {
            var parseTree = _parser.Parse("((24/1)/(2/3))");

            var parenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("24/1/(2/3)", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldSimplifyToExpressionWithoutParenthesis()
        {
            var parseTree = _parser.Parse("(((7*8)+(5*6))+(10/5))");

            var parenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("7*8+5*6+10/5", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreateExpressionWithMinimalParenthesisGivenExpressionWithVariablesAndFunctions()
        {
            var parseTree = _parser.Parse("(((7*a)+(5*cos(Pi)))+(10/sqrt(c+(d*5))))");

            var parenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("7*a+5*cos(Pi)+10/sqrt(c+d*5)", parenthesisVisitor.Result);
        }

        [Fact]
        public void CalculateExpressionWithConstantsCorrectly()
        {
            var parseTree = _parser.Parse("2 * Pi * Pi");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2 * Math.PI * Math.PI, calculator.Result);
        }

        [Fact]
        public void CalculatesAFunctionCallCorrectly()
        {
            var parseTree = _parser.Parse("2 * cos(Pi)");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2 * Math.Cos(Math.PI), calculator.Result);
        }

        [Fact]
        public void CalculatesFunctionCallsWithMultipleParametersCorrectly()
        {
            var parseTree = _parser.Parse("pow(sqrt(2), 2)");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2, calculator.Result, new MagnitudeDoubleComparer(0.00001));
        }

        [Fact]
        public void CalculatesComplexFunctionCallsWithMultipleParametersCorrectly()
        {
            var parseTree = _parser.Parse("pow(pow(2,2), sqrt(2) * sqrt(2)) * e");

            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(Math.E * 16, calculator.Result, new MagnitudeDoubleComparer(0.00001));
        }

        [Fact]
        public void ShouldParseVariableXAndCalculateWithDifferentValuesOfXCorrectly()
        {

            var parseTree = _parser.Parse("100 * x");

            var calculator = new CalculateVisitor();

            foreach (int x in Enumerable.Range(0, 100))
            {
                calculator.Variables["x"] = x;
                parseTree.Accept(calculator);
                Assert.Equal(100 * x, calculator.Result, new MagnitudeDoubleComparer(0.00001));
            }
        }

        [Fact]
        public void TheParserFactoryFunctionBuildsTheObjectTreeCorrectly()
        {
            var parser = Parser.Create();

            var parseTree = parser.Parse("145.2/ 3+7 -(8*45+22*(2-19))-88/8 + 17");
            var referenceParseTree = _parser.Parse("145.2/ 3+7 -(8*45+22*(2-19))-88/8 + 17");
            var calculator = new CalculateVisitor();

            referenceParseTree.Accept(calculator);
            var reference = calculator.Result;

            parseTree.Accept(calculator);
            Assert.Equal(reference, calculator.Result);
        }

        [Fact]
        public void TheParserSupportsPowerOperator()
        {
            var parseTree = _parser.Parse("2^10/2-12");
            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(500, calculator.Result);
        }

        [Fact]
        public void ThePowerOperatorIsLeftAssociative()
        {
            // There is no unique standard:
            // https://codeplea.com/exponentiation-associativity-options
            var parseTree = _parser.Parse("2^2^3");
            var calculator = new CalculateVisitor();
            parseTree.Accept(calculator);

            // Left Associative
            Assert.Equal(64, calculator.Result);

            // Right Associative
            Assert.NotEqual(256, calculator.Result);

            var fullParenthesisVisitor = new FullParenthesisVisitor();
            parseTree.Accept(fullParenthesisVisitor);

            Assert.Equal("((2^2)^3)", fullParenthesisVisitor.Result);

            var minimalParenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(minimalParenthesisVisitor);

            Assert.Equal("2^2^3", minimalParenthesisVisitor.Result);
        }

        [Fact]
        public void TheMinimalParenthesisVisitorWorksCorrectlyWithPower()
        {
            var parseTree = _parser.Parse("1-2*2^3*3-2");

            var minimalParenthesisVisitor = new MinimalParenthesisVisitor();
            parseTree.Accept(minimalParenthesisVisitor);

            Assert.Equal("1-2*2^3*3-2", minimalParenthesisVisitor.Result);
        }

        [Fact]
        public void TheMinimalParenthesisVisitorWorksWithMinusCorrectly()
        {
            var visitor = new MinimalParenthesisVisitor();

            _parser.Parse("1-(2-3)").Accept(visitor);
            Assert.Equal("1-(2-3)", visitor.Result);

            visitor.Clear();

            _parser.Parse("1-2-3").Accept(visitor);
            Assert.Equal("1-2-3", visitor.Result);
        }

        [Fact]
        public void TheMinimalParenthesisVisitorWorksWithDivideCorrectly()
        {
            var visitor = new MinimalParenthesisVisitor();

            _parser.Parse("1/(2/3)").Accept(visitor);
            Assert.Equal("1/(2/3)", visitor.Result);

            visitor.Clear();

            _parser.Parse("1/2/3").Accept(visitor);
            Assert.Equal("1/2/3", visitor.Result);
        }

        [Fact]
        public void TheReversePolishNotationVisitorCreatesExpressionsInRpn()
        {
            var visitor = new ReversePolishNotationVisitor();

            _parser.Parse("1+3*Pi-4").Accept(visitor);

            Assert.Equal("1 3 Pi * + 4 -", visitor.Result);
        }

        [Fact]
        public void TheLogarithmFunctionsAreWorkingAsExpected()
        {
            var visitor = new CalculateVisitor();

            _parser.Parse("lb(1024)").Accept(visitor);
            Assert.Equal(10, visitor.Result);

            _parser.Parse("ln(e^3)").Accept(visitor);
            Assert.Equal(3, visitor.Result);

            _parser.Parse("lg(100000)").Accept(visitor);
            Assert.Equal(5, visitor.Result);

            _parser.Parse("log(625,5)").Accept(visitor);
            Assert.Equal(4, visitor.Result);
        }

        [Fact]
        public void TheParserHandlesModuloOperatorCorrectly()
        {
            var visitor = new CalculateVisitor();

            _parser.Parse("119 % 100").Accept(visitor);
            Assert.Equal(19, visitor.Result);

            _parser.Parse("100 + 5 % 5").Accept(visitor);
            Assert.Equal(100, visitor.Result);

            _parser.Parse("5 * 5 % 7").Accept(visitor);
            Assert.Equal(4, visitor.Result);
        }

        [Fact]
        public void ANewLineIsTreatedAsWhiteSpace()
        {
            var visitor = new CalculateVisitor();

            _parser.Parse("119\n%\n100").Accept(visitor);
            Assert.Equal(19, visitor.Result);
        }

        [Fact]
        public void GivenAParseTreeEachNodeHasACorrectPosition()
        {
            var parseTree = _parser.Parse("cos(Pi/4)");

            if (parseTree is FunctionNode functionNode)
            {
                Assert.Equal(0, functionNode.Position.StartPosition);
                Assert.Equal(3, functionNode.Position.Length);

                Assert.Equal(3, functionNode.OpenParenthesisPosition.StartPosition);
                Assert.Equal(8, functionNode.ClosedParenthesisPosition.StartPosition);

                if (functionNode.Parameters.First() is DivisionOperator divisionOperator)
                {
                    Assert.Equal(6, divisionOperator.Position.StartPosition);
                    Assert.Equal(1, divisionOperator.Position.Length);
                    Assert.Equal(7, divisionOperator.Position.EndPosition);

                    if (divisionOperator.LeftOperand is VariableNode variableNode)
                    {
                        Assert.Equal(4, variableNode.Position.StartPosition);
                        Assert.Equal(2, variableNode.Position.Length);
                        Assert.Equal(6, variableNode.Position.EndPosition);
                        
                    }
                    else
                    {
                        Assert.True(false);
                    }
                } 
                else
                {
                    Assert.True(false);
                }
            }
            else
            {
                Assert.True(false);
            }

        }
    }
}