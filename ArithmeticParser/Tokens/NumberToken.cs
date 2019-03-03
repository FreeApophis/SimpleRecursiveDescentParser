namespace ArithmeticParser.Tokens
{
    public class NumberToken : Token
    {
        public NumberToken(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public override string ToString() => $"Number: {Value}";
    }
}
