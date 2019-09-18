using System;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    class PrintVisitor : INodeVisitor
    {
        public void Visit(LambdaExpression lambdaExpression)
        {
            throw new NotImplementedException();
        }

        public void Visit(Application application)
        {
            throw new NotImplementedException();
        }

        public void Visit(Variable variable)
        {
            throw new NotImplementedException();
        }
    }
}
