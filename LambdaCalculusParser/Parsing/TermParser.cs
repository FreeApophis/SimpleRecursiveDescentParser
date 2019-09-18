using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Tokens;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public class TermParser : IParser
    {
        public ApplicationParser ApplicationParser { get; set; }

        /// <summary>
        /// Parsing the following Production:
        /// Term        := Application | "λ" Identifier "." Term
        /// </summary>
        public ILambdaExpression Parse(TokenWalker walker)
        {
            if (walker.NextIs<LambdaToken>() == false)
            {
                return ApplicationParser.Parse(walker);
            }


            walker.Pop();
            var identifier = walker.Pop();
            if (identifier.Token is IdentifierToken variableName)
            {
                var variable = new Variable(variableName.Name);
                var dot = walker.Pop();

                if (dot.Token is DotToken)
                {
                    return new Term(variable, Parse(walker));
                }

                throw new Exception("Expecting '.' in expression, instead got: " + (walker.Peek() != null ? walker.Peek().ToString() : "End of expression"));

            }

            throw new Exception("Expecting an identfier in expression, instead got: " + (walker.Peek() != null ? walker.Peek().ToString() : "End of expression"));

        }
    }
}