﻿using Funcky.Lexer.Token;

namespace LambdaCalculusParser.Tokens
{
    public class IdentifierToken : IToken
    {
        public IdentifierToken(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString() => $"Identifier: {Name}";
    }
}