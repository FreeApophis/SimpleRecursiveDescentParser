using System;
using System.Collections.Generic;
using System.Linq;
using apophis.Lexer.Tokens;

namespace apophis.Lexer
{
    public class LinePositionCalculator
    {
        private readonly List<Position> _newLines;

        public LinePositionCalculator(List<Lexem> lexems)
        {
            _newLines = lexems
                .Where(l => l.IsLineBreak)
                .Select(l => l.Position)
                .ToList();
        }

        public LinePosition CalculateLinePosition(Lexem lexem)
        {
            return CalculateRelativePosition(
                LineNumber(lexem),
                lexem.Position,
                FindClosestNewLineBefore(lexem));
        }

        private int LineNumber(Lexem lexem)
        {
            return _newLines
                .Count(l => l.StartPosition < lexem.Position.StartPosition);
        }

        private LinePosition CalculateRelativePosition(int lineNumber, Position absolutePosition, Position newLinePosition)
        {
            return new LinePosition(
                ToHumanIndex(lineNumber),
                ToHumanIndex(absolutePosition.StartPosition - newLinePosition.EndPosition),
                absolutePosition.Length);
        }

        private int ToHumanIndex(int index)
        {
            return index + 1;
        }

        private Position FindClosestNewLineBefore(Lexem lexem)
        {
            return _newLines
                .LastOrDefault(l => l.StartPosition < lexem.Position.StartPosition);
        }

    }
}
