using System.Collections.Generic;
using LambdaCalculusParser.Nodes;
using Messerli.Lexer;

namespace LambdaCalculusParser
{
    public class ParserContext
    {
        public ParserContext(ITokenWalker tokenWalker)
        {
            TokenWalker = tokenWalker;
        }
        public ITokenWalker TokenWalker { get; }
        public Stack<Variable> BoundVariables { get; } = new();

    }
}