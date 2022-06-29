using System;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;
using Messerli.Lexer;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Variable   := Identifier
    /// </summary>
    public class VariableParser
    {
        public IParseNode Parse(ITokenWalker walker) 
            => ParseIdentifier(walker.Pop());

        private static IParseNode ParseIdentifier(Lexeme lexeme)
            => lexeme.Token is IdentifierToken
                ? new VariableNode(lexeme)
                : throw new ArgumentNullException();
    }
}
