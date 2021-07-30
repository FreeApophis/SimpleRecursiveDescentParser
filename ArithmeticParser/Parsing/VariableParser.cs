using System;
using apophis.Lexer;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

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
        public IParseNode Parse(TokenWalker walker) 
            => ParseIdentifier(walker.Pop());

        private static IParseNode ParseIdentifier(Lexem lexem)
            => lexem.Token is IdentifierToken
                ? new VariableNode(lexem)
                : throw new ArgumentNullException();
    }
}
