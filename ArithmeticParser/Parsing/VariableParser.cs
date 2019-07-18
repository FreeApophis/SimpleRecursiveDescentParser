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
            if (walker.Pop().Token is IdentifierToken identifier)
            {
                return new VariableNode(identifier.Name);
            }

            throw new ArgumentNullException();
        }
    }
}
