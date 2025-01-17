namespace ArithmeticParser.Test;

public class MagnitudeDoubleComparer(double maxDelta) : IEqualityComparer<double>
{
    public bool Equals(double x, double y)
    {
        return x - y < maxDelta;
    }

    public int GetHashCode(double obj)
    {
        throw new NotImplementedException();
    }
}