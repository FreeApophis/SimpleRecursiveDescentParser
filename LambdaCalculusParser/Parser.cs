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
    /// Term        := Application | "λ" Identifier "." Term
    /// Application := Atom Application | Ɛ
    /// Atom        := "(" Term ")" | Identifier
    /// Identifier  := [a-z]*
    /// </summary>
    public class Parser
    {
        private readonly TokenWalker _tokenWalker;
        private readonly TermParser _termParser;

        public Parser(TokenWalker tokenWalker, TermParser termParser)
        {
            _tokenWalker = tokenWalker;
            _termParser = termParser;
        }
        public ILambdaExpression Parse(string expression)
        {
            _tokenWalker.Scan(expression, lexems => lexems.Where(t => t.Token.GetType() != typeof(WhiteSpaceToken)));

            return Parse(_tokenWalker);
        }
        private ILambdaExpression Parse(TokenWalker walker)
        {
            return _termParser.Parse(walker);
        }


        public static Parser Create()
        {
            // Create the object tree without DI Framework
            var termParser = new TermParser();
            var atomParser = new AtomParser(termParser);
            var applicationParser = new ApplicationParser(atomParser);
            termParser.ApplicationParser = applicationParser;
            var lexerRules = new LexerRules();
            var tokenizer = new Tokenizer(lexerRules, s => new LexerReader(s));
            var tokenWalker = new TokenWalker(tokenizer, () => new EpsilonToken());
            return new Parser(tokenWalker, termParser);
        }
    }
}
