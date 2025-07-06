using ESGScoringApp.Models;

namespace ESGScoringApp.Interfaces;

public interface IExecuteOperator
{
    double? Evaluate(MetricConfig metric, Dictionary<string, double?> context,
        Dictionary<string, double?> metricResults);
}