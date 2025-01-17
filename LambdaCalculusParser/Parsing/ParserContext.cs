using Funcky.Lexer;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser;

public class ParserContext(ILexemeWalker walker)
{
    public ILexemeWalker Walker { get; } = walker;

    public Stack<Variable> BoundVariables { get; } = new();
}
