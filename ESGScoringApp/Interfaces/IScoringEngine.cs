using System.Collections;
using ESGScoringApp.Results;

namespace ESGScoringApp.Interfaces;

public interface IScoringEngine
{
    List<Dictionary<string, object>> Score(DataSourceResult dataSourceResult);
}