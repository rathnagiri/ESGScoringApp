namespace ESGScoringApp.Interfaces;

public interface ICsvDataLoader
{
    Dictionary<(string, int), Dictionary<string, double?>> Load(string filePath, string dataType);
}