using ESGScoringApp.Interfaces;
using ESGScoringApp.Results;
using Microsoft.Extensions.Logging;

namespace ESGScoringApp;
public class ScoringEngine(ILogger<ScoringEngine> logger, IExecuteOperator operatorFn) : IScoringEngine
{
    public List<Dictionary<string, object>> Score(DataSourceResult dataSourceResult)
    {
        logger.LogInformation("Started config-based scoring engine");

        if (dataSourceResult?.DataSources == null || dataSourceResult.Config?.Metrics == null)
        {
            logger.LogWarning("Invalid input: Missing data sources or metric configuration.");
            return [];
        }

        var dataSources = dataSourceResult.DataSources;
        var config = dataSourceResult.Config;
        var allKeys = dataSources.Values.SelectMany(ds => ds.Keys).Distinct().ToList();
        var results = new List<Dictionary<string, object>>();

        foreach (var (companyId, year) in allKeys)
        {
            var context = BuildContext(dataSources, companyId, year);
            var metricResults = new Dictionary<string, double?>();

            foreach (var metric in config.Metrics)
            {
                try
                {
                    metricResults[metric.Name] = operatorFn.Evaluate(metric, context, metricResults);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error evaluating metric '{metric.Name}' for {companyId}/{year}");
                    metricResults[metric.Name] = null;
                }
            }

            var row = new Dictionary<string, object>
            {
                ["company_id"] = companyId,
                ["year"] = year
            };

            foreach (var metric in config.Metrics)
            {
                row[metric.Name] = metricResults[metric.Name];
            }

            results.Add(row);
        }

        logger.LogInformation("Successfully finished config-based scoring engine");
        return results;
    }

    private Dictionary<string, double?> BuildContext(
        Dictionary<string, Dictionary<(string, int), Dictionary<string, double?>>> dataSources,
        string companyId, int year)
    {
        var context = new Dictionary<string, double?>();

        foreach (var ds in dataSources.Values)
        {
            if (ds.TryGetValue((companyId, year), out var vals))
            {
                foreach (var kv in vals)
                    context[kv.Key] = kv.Value;
            }
        }

        return context;
    }
}