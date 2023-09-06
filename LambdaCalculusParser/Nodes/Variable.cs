﻿using Funcky.Lexer;
using Funcky.Monads;
using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Nodes
{
    public class Variable : ILambdaExpression
    {
        public string Name { get; }
        public Option<int> DeBruinIndex { get; }

        public Variable(string name, Option<int> deBruinIndex = default)
        {
            Name = name;
            DeBruinIndex = deBruinIndex;
        }

        public void Accept(ILambdaExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Position Position { get; }

        public override string ToString()
        {
            return $"{nameof(Variable)}: {this.ToNormalForm()}";
        }
    }
}