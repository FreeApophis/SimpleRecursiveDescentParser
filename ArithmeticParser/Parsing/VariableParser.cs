using System;
using apophis.Lexer;
using ArithmeticParser.Lexing;
using ArithmeticParser.Nodes;
using ArithmeticParser.Tokens;

namespace ArithmeticParser.Parsing
{
    /// <summary>
    /// Parsing the following Production:
    /// Variable   := Identifier
    /// </summary>
    public class VariableParser : IParser
    {
        /// <summary>
        /// Overloaded Parse function to parse a Function
        /// </summary>
        /// <param name="walker">Lexer input</param>
        /// <returns></returns>
        public IParseNode Parse(TokenWalker walker)
        {
            var lexem = walker.Pop();
            if (lexem.Token is IdentifierToken)
            {
                return new VariableNode(lexem);
            }

            throw new ArgumentNullException();
        }
    }
}
