namespace apophis.Lexer
{
    /// <summary>
    /// Represents the position of a Lexem.
    /// </summary>
    public readonly struct Position
    {
        public Position(int start, int length)
        {
            StartPosition = start;
            EndPosition = start + length;
            Length = length;
        }

        /// <summary>
        /// Represents the position of the first character of the lexem, countent in number of characters.
        /// </summary>
        public int StartPosition { get; }

        /// <summary>
        /// Represents the position of the first character after the lexem, countent in number of characters.
        /// </summary>
        public int EndPosition { get; }

        /// <summary>
        /// Represents the length of the lexem.
        /// </summary>
        public int Length { get; }
    }
}
