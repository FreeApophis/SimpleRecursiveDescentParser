namespace ArithmeticParser.Nodes;

/// <summary>
/// The Precedence enum represents the different precedence levels of the operators.
/// </summary>
public enum Precedence
{
    /// <summary>
    /// The precedence level for arithmetic line operations (+, -).
    /// </summary>
    Line,

    /// <summary>
    /// The precedence level for arithmetic point operations (*, /, %).
    /// </summary>
    Point,

    /// <summary>
    /// The precedence level for arithmetic power operation (^).
    /// </summary>
    Power,
}