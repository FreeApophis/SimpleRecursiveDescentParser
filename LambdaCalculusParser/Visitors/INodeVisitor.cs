using System;
using System.Collections.Generic;
using System.Text;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    interface INodeVisitor
    {
        void Visit(LambdaExpression lambdaExpression);
        void Visit(Application application);
        void Visit(Variable variable);

    }
}
