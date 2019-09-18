using System;
using Xunit;

namespace LambdaCalculusParser.Test
{
    public class LambdaCalculusParserTest
    {
        //private readonly Parser _parser;

        public LambdaCalculusParserTest()
        {
            //_parser = new CompositionRoot()
            //    .Build()
            //    .Resolve<Parser>();
        }

        [Fact]
        public void ParseZeroLambdaExpression()
        {
            var churchNumeralZero = @"λf.λx.x";

            //var parseTree = _parser.Parse(churchNumeralZero);

            //var applicator = new ApplicationVisitor();
            //parseTree.Accept(applicator);
        }

    }
}
