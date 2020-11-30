using System.Collections.Generic;
using System.Linq;

namespace apophis.Lexer
{
    public interface ILinePositionCalculator
    {
        LinePosition CalculateLinePosition(Lexem lexem);

        LinePosition CalculateLinePosition(int absolutePosition);
    }

    public class LinePositionCalculator : ILinePositionCalculator
    {
        public delegate ILinePositionCalculator Factory(List<Lexem> lexems);

        private readonly List<Position> _newLines;

        public LinePositionCalculator(List<Lexem> lexems) =>
            _newLines = lexems
                .Where(l => l.IsLineBreak)
                .Select(l => l.Position)
                .ToList();

        public LinePosition CalculateLinePosition(Lexem lexem)
            => CalculateRelativePosition(
                LineNumber(lexem.Position.StartPosition),
                lexem.Position.StartPosition,
                lexem.Position.Length,
                FindClosestNewLineBefore(lexem.Position.StartPosition));

        public LinePosition CalculateLinePosition(int absolutePosition)
            => CalculateRelativePosition(
                LineNumber(absolutePosition),
                absolutePosition,
                1,
                FindClosestNewLineBefore(absolutePosition));

        private int LineNumber(int absolutePosition)
            => _newLines
                .Count(l => l.StartPosition < absolutePosition);

        private LinePosition CalculateRelativePosition(int lineNumber, int absolutePosition, int length, Position newLinePosition)
            => new(
                ToHumanIndex(lineNumber),
                ToHumanIndex(absolutePosition - newLinePosition.EndPosition),
                length);

        private int ToHumanIndex(int index)
            => index + 1;

        private Position FindClosestNewLineBefore(int position)
            => _newLines
                .LastOrDefault(l => l.StartPosition < position);
    }
}
