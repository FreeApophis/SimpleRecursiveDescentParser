using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    public interface IParser
    {
        IParseNode Parse(TokenWalker walker);
    }
}