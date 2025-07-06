using CommandLine;

namespace ESGScoringApp.Models;

public class Options
{
    [Option(longName: "baseDirectoryPath", shortName: 'b', Required = false,
        HelpText = "Path to base directory containing data and config directories, defaults to the current directory.")]
    public string BaseDirectoryPath { get; set; } = string.Empty;

    [Option(longName: "emission", shortName: 'e', Required = false,
        HelpText = "emissions data file", Default = "emissions_data.csv")]
    public string EmissionDataFile { get; set; }
    
    [Option(longName: "disclosure", shortName: 'd', Required = false,
        HelpText = "disclosure data file", Default = "disclosure_data.csv")]
    public string DisclosureDataFile { get; set; }
    
    [Option(longName: "waste", shortName: 'w', Required = false,
        HelpText = "waster data file", Default = "waste_data.csv")]
    public string WasteDataFile { get; set; }
    
    [Option(longName: "config", shortName: 'c', Required = false,
        HelpText = "json config file", Default = "score_1.yaml")]
    public string ConfigFile { get; set; }
}