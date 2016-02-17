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
    }

    public class MinusToken : OperatorToken
    {
    }

    public class MultiplicationToken : OperatorToken
    {
    }

    public class DivideToken : OperatorToken
    {
    }

    public abstract class ParenthesisToken : Token
    {

    }

    public class OpenParenthesisToken : ParenthesisToken
    {
    }

    public class ClosedParenthesisToken : ParenthesisToken
    {
    }


    public class NumberToken : Token
    {
        public NumberToken(double value)
        {
            Value = value;
        }

        public double Value { get; private set; }
    }
}
