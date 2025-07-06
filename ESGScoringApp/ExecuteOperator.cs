using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;

namespace ESGScoringApp;

public class ExecuteOperator : IExecuteOperator
{
    private readonly Dictionary<string, IOperatorStrategy> _strategies;

    public ExecuteOperator(IEnumerable<IOperatorStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.Name.ToLowerInvariant());
    }

    public double? Evaluate(MetricConfig metric, Dictionary<string, double?> context, Dictionary<string, double?> metricResults)
    {
        if (metric == null || metric.Operation == null || string.IsNullOrWhiteSpace(metric.Operation.Type))
            throw new ArgumentException("Invalid metric configuration");

        var operatorKey = metric.Operation.Type.ToLowerInvariant();

        if (!_strategies.TryGetValue(operatorKey, out var strategy))
            throw new InvalidOperationException($"Unsupported operation type: {operatorKey}");

        var parameters = metric.Operation.Parameters.Select(p =>
        {
            if (p.Source.StartsWith("self.", StringComparison.OrdinalIgnoreCase))
            {
                var mName = p.Source[5..];
                return metricResults.GetValueOrDefault(mName);
            }

            return context.GetValueOrDefault(p.Source);
        }).ToList();

        return strategy.Evaluate(parameters);
    }
}