using System;
using LambdaCalculusParser.Nodes;

namespace LambdaCalculusParser.Visitors
{
    public class AlphaReductionEventArgs
    {
    }

    public class BetaReductionEventArgs
    {
    }

    public class EtaReductionEventArgs
    {
    }

    public class InterpreterVisitor : ILambdaExpressionVisitor
    {
        public delegate void AlphaReductionEventHandler(object sender, AlphaReductionEventArgs e);
        public delegate void BetaReductionEventHandler(object sender, BetaReductionEventArgs e);
        public delegate void EtaReductionEventHandler(object sender, EtaReductionEventArgs e);

        public event AlphaReductionEventHandler AlphaReductionEvent;
        public event BetaReductionEventHandler BetaReductionEvent;
        public event EtaReductionEventHandler EtaReductionEvent;

        public ILambdaExpression Result;


        public void Visit(Abstraction abstraction)
        {
            Result = abstraction;
        }

        public void Visit(Application application)
        {
            throw new NotImplementedException();
        }

        public void Visit(Variable variable)
        {
            Result = variable;
        }

        private void AlphaReduction()
        {
            AlphaReductionEvent?.Invoke(this, new AlphaReductionEventArgs());
        }

        private void BetaReduction()
        {
            BetaReductionEvent?.Invoke(this, new BetaReductionEventArgs());
        }

        private void EtaReduction()
        {
            EtaReductionEvent?.Invoke(this, new EtaReductionEventArgs());
        }
    }
}
