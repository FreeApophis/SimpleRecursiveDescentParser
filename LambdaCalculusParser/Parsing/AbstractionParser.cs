using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public class AbstractionParser : IParser
    {
        public ApplicationParser ApplicationParser { get; set; }

        /// <summary>
        /// Parsing the following Production:
        /// Abstraction        := Application | "λ" Identifier "." Abstraction
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<LambdaToken>() == false)
            {
                return ApplicationParser.Parse(walker);
            }


            walker.Pop();

            if (walker.Pop().Token is IdentifierToken variableName)
            {
                var variable = new Variable(variableName.Name);

                walker.Consume<DotToken>();

                return new Abstraction(variable, Parse(walker));

            }

            throw new Exception("Expecting an identfier in expression, instead got: " + (walker.Peek() != null ? walker.Peek().Token.ToString() : "End of expression"));

        }
    }
}