using ESGScoringApp.Results;

namespace ESGScoringApp.Interfaces;

public interface ICsvWriterHelper
{
    void WriteResultsToFile(string path, DataSourceResult dataSourceResult, List<Dictionary<string, object>> results);
}