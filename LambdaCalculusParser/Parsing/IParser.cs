using apophis.Lexer;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Parsing
{
    public interface IParser
    {
        ILambdaExpression Parse(TokenWalker walker);
    }
}