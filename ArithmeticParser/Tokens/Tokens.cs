namespace FormelParser
{
    public abstract class Token
    {
    }

    public abstract class OperatorToken : Token
    {
    }
    public class PlusToken : OperatorToken
    {
        public override string ToString() => "Addition Operator";
    }

    public class MinusToken : OperatorToken
    {
        public override string ToString() => "Subtraction Operator";
    }

    public class MultiplicationToken : OperatorToken
    {
        public override string ToString() => "Multiplication Operator";
    }

    public class DivideToken : OperatorToken
    {
        public override string ToString() => "Division Operator";
    }

    public abstract class ParenthesisToken : Token
    {

    }

    public class OpenParenthesisToken : ParenthesisToken
    {
        public override string ToString() => "Opening Parenthesis";
    }

    public class ClosedParenthesisToken : ParenthesisToken
    {
        public override string ToString() => "Closing Parenthesis";
    }

    public class CommaToken : ParenthesisToken
    {
        public override string ToString() => "Comma";
    }

    public class NumberToken : Token
    {
        public NumberToken(double value)
        {
            Value = value;
        }

        public double Value { get; private set; }

        public override string ToString() => $"Number: {Value}";
    }

    public class IdentifierToken : Token
    {
        public IdentifierToken(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString() => $"Identifier: {Name}";

    }
}
