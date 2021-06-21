using System.Collections.Generic;
using System.Linq;
using apophis.Lexer.Tokens;
using Funcky.Extensions;
using Funcky.Monads;

namespace apophis.Lexer.Rules
{
    public class SimpleLexerRule<TToken> : ILexerRule
        where TToken : IToken, new()
    {
        private readonly string _textSymbol;
        private readonly bool _isTextSymbol;

        public SimpleLexerRule(string textSymbol)
        {
            _textSymbol = textSymbol;
            _isTextSymbol = textSymbol.All(char.IsLetter);
        }

        public int Weight
            => _textSymbol.Length;

        public Option<Lexem> Match(ILexerReader reader)
            => MatchLexem(reader, reader.Position);

        private Option<Lexem> MatchLexem(ILexerReader reader, int startPosition)
            => IsSymbolMatchingReader(reader) && (IsOperator() || HasWordBoundary(reader))
                ? ConsumeLexem(reader, startPosition)
                : Option<Lexem>.None();

        private Option<Lexem> ConsumeLexem(ILexerReader reader, int startPosition)
        {
            _textSymbol.ForEach(_ => reader.Read());

            return CreateLexem(startPosition);
        }

        public bool IsActive(List<Lexem> context)
            => true;

        private bool HasWordBoundary(ILexerReader reader)
            // we do not want to extract key words in the middle of a word, so a symbol must have ended.
            // Which means after a textsymbol must come something other than a digit or a letter.
            => reader.Peek(_textSymbol.Length).Match(true, NonLetterOrDigit);

        private bool IsOperator()
            => !_isTextSymbol;

        private bool NonLetterOrDigit(char character)
            => !char.IsLetterOrDigit(character);

        private bool IsSymbolMatchingReader(ILexerReader reader)
            => _textSymbol.Select((character, index) => new { character, index })
                .All(t => reader.Peek(t.index).Match(false, c => c == t.character));

        private Lexem CreateLexem(int start)
            => CreateLexemFromToken(start, new TToken());

        private Lexem CreateLexemFromToken(int start, TToken token)
            => new(token, new Position(start, _textSymbol.Length), token is ILineBreakToken);
    }
}