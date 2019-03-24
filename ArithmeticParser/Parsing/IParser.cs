using ArithmeticParser.Nodes;

namespace ArithmeticParser.Parsing
{
    public interface IParser
    {
        IParseNode Parse();
    }
}