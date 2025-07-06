using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;

namespace ESGScoringApp;

public class ConfigurationMapper : IConfigurationMapper
{
    public ConfigurationData? Map(Options options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var configData = new ConfigurationData();
        var basePath = options.BaseDirectoryPath;

        if (string.IsNullOrEmpty(basePath)) basePath = Directory.GetCurrentDirectory();

        configData.DisclosureFilePath = Path.Combine(basePath, "Data", options.DisclosureDataFile);
        configData.EmissionFilePath = Path.Combine(basePath, "Data", options.EmissionDataFile);
        configData.WasteFilePath = Path.Combine(basePath, "Data", options.WasteDataFile);
        configData.ConfigFilePath = Path.Combine(basePath, "Config", options.ConfigFile);
        configData.ResultsFilePath = Path.Combine(basePath, "score-result.csv");

        return configData;
    }
}