using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;
using Microsoft.Extensions.Logging;

namespace ESGScoringApp;

public class CommandlineProcessor(
    IConfigurationMapper mapper,
    IDataSourceLoader dataSourceLoader,
    IScoringEngine scoringEngine,
    ICsvWriterHelper csvWriterHelper,
    ILogger<CommandlineProcessor> logger) : ICommandlineProcessor
{
    public void Run(Options options)
    {
        ArgumentNullException.ThrowIfNull(options);
        try
        {
            var configData = mapper.Map(options);
            var dataSourceResult = dataSourceLoader.Load(configData);
            
            if (!dataSourceResult.IsSuccess)
            {
                dataSourceResult.Errors.ForEach(err => logger.LogError(err));
                return;
            }
            var results = scoringEngine.Score(dataSourceResult);
            csvWriterHelper.WriteResultsToFile(configData.ResultsFilePath, dataSourceResult, results);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process scoring data");
            throw;
        }
    }
}