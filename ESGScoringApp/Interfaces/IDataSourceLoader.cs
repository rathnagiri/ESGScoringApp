using ESGScoringApp.Models;
using ESGScoringApp.Results;

namespace ESGScoringApp.Interfaces;

public interface IDataSourceLoader
{
    DataSourceResult Load(ConfigurationData config);
}