﻿using Funcky.Lexer.Token;

namespace ArithmeticParser.Tokens;

public class NumberToken : IToken
{
    public NumberToken(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public override string ToString()
        => $"Number: {Value}";
}