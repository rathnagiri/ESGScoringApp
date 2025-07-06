using ESGScoringApp.Models;

namespace ESGScoringApp.Results;

public class DataSourceResult
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public Dictionary<string, Dictionary<(string, int), Dictionary<string, double?>>> DataSources { get; set; }
    public ScoreConfig Config { get; set; }
}