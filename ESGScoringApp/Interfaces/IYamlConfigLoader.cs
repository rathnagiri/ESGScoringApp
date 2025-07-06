using ESGScoringApp.Models;

namespace ESGScoringApp.Interfaces;

public interface IYamlConfigLoader
{
    ScoreConfig Load(string yamlPath);
}