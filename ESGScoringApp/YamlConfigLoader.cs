using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ESGScoringApp;

public class YamlConfigLoader : IYamlConfigLoader
{
    public ScoreConfig Load(string yamlPath)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        using var reader = new StreamReader(yamlPath);
        return deserializer.Deserialize<ScoreConfig>(reader);
    }
}