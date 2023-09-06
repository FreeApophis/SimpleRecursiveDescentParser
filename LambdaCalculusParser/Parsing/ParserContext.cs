using Funcky.Lexer;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser;

public class ParserContext
{
    public ParserContext(ILexemeWalker walker)
    {
        Walker = walker;
    }

    public ILexemeWalker Walker { get; }

    public Stack<Variable> BoundVariables { get; } = new();

}