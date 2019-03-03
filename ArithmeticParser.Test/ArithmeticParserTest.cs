using System;
using System.Linq;
using FormelParser;
using Xunit;

namespace ArithmeticParser.Test
{
    public class ArithmeticParserTest
    {

        [Fact]
        public void ParseSimpleArithmeticExpressionCorrectly()
        {
            Parser parser = new Parser("5+17*22");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(379.0, calculator.Result);
        }

        [Fact]
        public void ParseSingleNumberCorrectly()
        {
            Parser parser = new Parser("22");
            var parseTree = parser.Parse();

            var number = parseTree as NumberNode;
            Assert.NotNull(number);
            Assert.Equal(22.0, number.Number);

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(22.0, calculator.Result);

            var parenthesisVisitor = new FormelParser.Visitors.FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("22", parenthesisVisitor.Result);
        }

        [Fact]
        public void ParseSimpleFloatCorrectly()
        {
            Parser parser = new Parser("3.14159265");
            var parseTree = parser.Parse();

            var number = parseTree as NumberNode;
            Assert.NotNull(number);
            Assert.Equal(3.14159265, number.Number);

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(3.14159265, calculator.Result);

            var parenthesisVisitor = new FormelParser.Visitors.FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("3.14159265", parenthesisVisitor.Result);
        }

        [Fact]
        public void ParseSimpleAdditionCorrectly()
        {
            Parser parser = new Parser("1+2+3+4+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(210.0, calculator.Result);
        }

        [Fact]
        public void ParseSimpleSubtractionCorrectly()
        {
            Parser parser = new Parser("100-1-2-3-4-5-6-7-8-9)");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(55.0, calculator.Result);

        }

        [Fact]
        public void ParseSimpleMultiplicationCorrectly()
        {
            Parser parser = new Parser("1*2*3*4*5*6*7");

            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(5040.0, calculator.Result);
        }

        [Fact]
        public void ParseSimpleDivisionCorrectly()
        {
            Parser parser = new Parser("(1/2)/(1/10)/2");

            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(2.5, calculator.Result);
        }

        [Fact]
        public void ParseComplexArithmeticExampleCorrectly()
        {
            Parser parser = new Parser("145.2/3+7-(8*45+22*(2-19))-88/8 + 17");

            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);

            Assert.Equal(75.4, calculator.Result);

            var parenthesisVisitor = new FormelParser.Visitors.FullParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("(((((145.2/3)+7)-((8*45)+(22*(2-19))))-(88/8))+17)", parenthesisVisitor.Result);
        }

        [Fact]
        public void CreatesArithmeticExpressionWithMinimalAmountOfParenthesis()
        {
            Parser plusParser = new Parser("(1+(2+(3+4)))");
            var plusParseTree = plusParser.Parse();

            var parenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            plusParseTree.Accept(parenthesisVisitor);

            Assert.Equal("1+2+3+4", parenthesisVisitor.Result);

            Parser minusParser = new Parser("(-1-(2-(3-4)))");
            var minusParseTree = minusParser.Parse();

            var minimalParenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            minusParseTree.Accept(minimalParenthesisVisitor);

            Assert.Equal("-1-(2-(3-4))", minimalParenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreateMultiplicationWithMinimalAmountOfParenthesis()
        {
            Parser parser = new Parser("6*(1+2+3)");
            var parseTree = parser.Parse();

            var parenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("6*(1+2+3)", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreatesDivisionWithMinimalAmountOfParenthesis()
        {
            Parser parser = new Parser("((24/1)/(2/3))");
            var parseTree = parser.Parse();

            var parenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("(24/1)/(2/3)", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldSimplifyToExpressionWithoutParenthesis()
        {
            Parser parser = new Parser("(((7*8)+(5*6))+(10/5))");
            var parseTree = parser.Parse();

            var parenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("7*8+5*6+10/5", parenthesisVisitor.Result);
        }

        [Fact]
        public void ShouldCreateExpressionWithMinimalParenthesisGivenExpressionWithVariablesAndFunctions()
        {
            Parser parser = new Parser("(((7*a)+(5*cos(Pi)))+(10/sqrt(c+(d*5))))");
            var parseTree = parser.Parse();

            var parenthesisVisitor = new FormelParser.Visitors.MinimalParenthesisVisitor();
            parseTree.Accept(parenthesisVisitor);

            Assert.Equal("7*a+5*cos(Pi)+10/sqrt(c+d*5)", parenthesisVisitor.Result);
        }

        [Fact]
        public void CalculateExpressionWithConstantsCorrectly()
        {
            Parser parser = new Parser("2 * Pi * Pi");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2 * Math.PI * Math.PI, calculator.Result);
        }

        [Fact]
        public void CalculatesAFunctionCallCorrectly()
        {
            Parser parser = new Parser("2 * cos(Pi)");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2 * Math.Cos(Math.PI), calculator.Result);
        }

        [Fact]
        public void CalculatesFunctionCallsWithMultipleParametersCorrectly()
        {
            Parser parser = new Parser("pow(sqrt(2), 2)");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(2, calculator.Result, new MagnitudeDoubleComparer(0.00001));
        }

        [Fact]
        public void CalculatesComplexFunctionCallsWithMultipleParametersCorrectly()
        {
            Parser parser = new Parser("pow(pow(2,2), sqrt(2) * sqrt(2)) * e");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();
            parseTree.Accept(calculator);


            Assert.Equal(Math.E * 16, calculator.Result, new MagnitudeDoubleComparer(0.00001));
        }

        [Fact]
        public void ShouldParseVariableXAndCalculateWithDifferentValuesOfXCorrectly()
        {

            Parser parser = new Parser("100 * x");
            var parseTree = parser.Parse();

            var calculator = new FormelParser.Visitors.CalculateVisitor();

            foreach (int x in Enumerable.Range(0, 100))
            {
                calculator.Variables["x"] = x;
                parseTree.Accept(calculator);
                Assert.Equal(100 * x, calculator.Result, new MagnitudeDoubleComparer(0.00001));
            }
        }
    }
}