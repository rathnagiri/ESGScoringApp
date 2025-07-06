using ESGScoringApp.Interfaces;
using ESGScoringApp.Results;
using Microsoft.Extensions.Logging;

namespace ESGScoringApp;

public class CsvWriterHelper(ILogger<CsvWriterHelper> logger) : ICsvWriterHelper
{
    public void WriteResultsToFile(string resultsFilepath, 
        DataSourceResult dataSourceResult, List<Dictionary<string, object>> results)
    {
        try
        {
            using var writer = new StreamWriter(resultsFilepath);
            var headers = new List<string> { "company_id", "year" };
            headers.AddRange(dataSourceResult.Config.Metrics.Select(m => m.Name));
            writer.WriteLine(string.Join(",", headers));
            foreach (var row in results)
            {
                writer.WriteLine(string.Join(",", headers.Select(h => row[h])));
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error writing result to file");
            throw;
        }
    }
}