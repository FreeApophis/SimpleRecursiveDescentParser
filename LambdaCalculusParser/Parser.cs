using System.Linq;
using apophis.Lexer;
using LambdaCalculusParser.Lexing;
using LambdaCalculusParser.Nodes;
using LambdaCalculusParser.Parsing;
using LambdaCalculusParser.Tokens;

namespace LambdaCalculusParser
{

    /// <summary>
    /// This is a Recursive Descent Parser for arithmetic expressions with real numbers with the following grammar in EBNF
    ///
    /// Abstraction        := Application | "λ" Identifier "." Abstraction
    /// Application := Atom Application | Ɛ
    /// Atom        := "(" Abstraction ")" | Identifier
    /// Identifier  := [a-z]*
    /// </summary>
    public class Parser
    {
        private readonly TokenWalker _tokenWalker;
        private readonly AbstractionParser _abstractionParser;

        public Parser(TokenWalker tokenWalker, AbstractionParser abstractionParser)
        {
            _tokenWalker = tokenWalker;
            _abstractionParser = abstractionParser;
        }
        public ILambdaExpression Parse(string expression)
        {
            _tokenWalker.Scan(expression, lexems => lexems.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)));

            return Parse(_tokenWalker);
        }
        private ILambdaExpression Parse(TokenWalker walker)
        {
            return _abstractionParser.Parse(walker);
        }


        public static Parser Create()
        {
            // Create the object tree without DI Framework
            var abstractionParser = new AbstractionParser();
            var atomParser = new AtomParser(abstractionParser);
            var applicationParser = new ApplicationParser(atomParser);
            abstractionParser.ApplicationParser = applicationParser;
            var lexerRules = new LexerRules();
            var tokenizer = new Tokenizer(lexerRules, s => new LexerReader(s));
            var tokenWalker = new TokenWalker(tokenizer, () => new EpsilonToken());
            return new Parser(tokenWalker, abstractionParser);
        }
    }
}
