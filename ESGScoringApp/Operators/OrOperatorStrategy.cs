using ESGScoringApp.Interfaces;

namespace ESGScoringApp.Operators;

public class OrOperatorStrategy : IOperatorStrategy
{
    public string Name => "or";

    public double? Evaluate(List<double?> parameters)
        => parameters.FirstOrDefault(x => x.HasValue);
}