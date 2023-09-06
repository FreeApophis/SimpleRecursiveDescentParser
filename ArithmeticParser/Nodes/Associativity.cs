namespace ArithmeticParser.Nodes;

/// <summary>
/// An enum representing the associativity of an operator
/// </summary>
public enum Associativity
{
    /// <summary>
    /// Left associative (a + b) + c
    /// </summary>
    Left,

    /// <summary>
    /// Right associative a + (b + c)
    /// </summary>
    Right
}