using LambdaCalculusParser.Visitors;

namespace LambdaCalculusParser.Test;

internal class ReductionEventCollector
{
    public List<string> Events { get; } = new();

    public void OnAlphaReductionEvent(object sender, AlphaReductionEventArgs e)
    {
        Events.Add("α");
    }

    public void OnBetaReductionEvent(object sender, BetaReductionEventArgs e)
    {
        Events.Add("β");
    }

    public void OnEtaReductionEvent(object sender, EtaReductionEventArgs e)
    {
        Events.Add("η");
    }
}