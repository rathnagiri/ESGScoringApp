using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;
using ESGScoringApp.Results;
using Microsoft.Extensions.Logging;

namespace ESGScoringApp;

// encapsulates validation logic for various data sources
public class DataSourceLoader(
    IFileValidator fileValidator,
    IYamlConfigLoader yamlConfigLoader,
    ICsvDataLoader csvDataLoader,
    ILogger<DataSourceLoader> logger) : IDataSourceLoader
{
    public DataSourceResult Load(ConfigurationData config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var result = new DataSourceResult();

        try
        {
            var filePaths = new Dictionary<string, string>
            {
                ["emissions"] = config.EmissionFilePath,
                ["waste"] = config.WasteFilePath,
                ["disclosure"] = config.DisclosureFilePath,
                ["config"] = config.ConfigFilePath,
            };

            var validationErrors = fileValidator.ValidateFilesExist(filePaths);
            if (validationErrors.Count > 0)
            {
                result.Errors = validationErrors;
                return result;
            }
       
            // load yaml config file
            try
            {
                var yamlConfig = yamlConfigLoader.Load(config.ConfigFilePath);
                result.Config = yamlConfig;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Failed to load config from '{config.ConfigFilePath}': {ex.Message}");
                logger.LogError(ex, "Error loading config");
                return result;
            }
        
            // Load CSV files
            var dataSources = new Dictionary<string, Dictionary<(string, int), Dictionary<string, double?>>>();
            filePaths.Remove("config");
        
            foreach (var dataFile in filePaths)
            {
                var dataType = dataFile.Key;
                var filePath = dataFile.Value;

                try
                {
                    var data = csvDataLoader.Load(filePath, dataType);
                    dataSources[dataType] = data;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Failed to load {dataType} data from '{filePath}': {ex.Message}");
                    logger.LogError(ex, $"Error loading {dataType} data");
                }
            }
            result.DataSources = dataSources;
            result.IsSuccess = true;
            logger.LogInformation("Data loading completed successfully");
            return result;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Errors.Add($"Unexpected error during data loading: {ex.Message}");
            logger.LogError(ex, "Unexpected error during data loading");
            return result;
        }
    }
}