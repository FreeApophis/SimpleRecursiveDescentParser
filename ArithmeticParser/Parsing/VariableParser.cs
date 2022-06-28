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
        /// <summary>
        /// Overloaded Parse function to parse a Function
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(ITokenWalker walker) 
            => ParseIdentifier(walker.Pop());

        private static IParseNode ParseIdentifier(Lexeme lexeme)
            => lexeme.Token is IdentifierToken
                ? new VariableNode(lexeme)
                : throw new ArgumentNullException();
    }
}
