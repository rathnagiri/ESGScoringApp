using ESGScoringApp.Models;

namespace ESGScoringApp.Interfaces;

public interface IConfigurationMapper
{
    ConfigurationData? Map(Options options);
}