namespace apophis.Lexer
{
    public class LinePosition
    {
        public LinePosition(int line, int column, int length)
        {
            Line = line;
            Column = column;
            Length = length;
        }

        public int Line { get; }

        public int Column { get; }

        public int Length { get; }
    }
}
