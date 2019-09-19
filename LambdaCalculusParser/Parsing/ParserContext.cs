using System.Collections.Generic;
using apophis.Lexer;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser
{
    public class ParserContext
    {
        public ParserContext(TokenWalker tokenWalker)
        {
            TokenWalker = tokenWalker;
        }
        public TokenWalker TokenWalker { get; }
        public Stack<Variable> BoundVariables { get; } = new Stack<Variable>();

    }
}