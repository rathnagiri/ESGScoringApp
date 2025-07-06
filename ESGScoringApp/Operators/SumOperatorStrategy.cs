using ESGScoringApp.Interfaces;

namespace ESGScoringApp.Operators;

public class SumOperatorStrategy : IOperatorStrategy
{
    public string Name => "sum";

    public double? Evaluate(List<double?> parameters)
        => parameters.Sum(x => x ?? 0);
}