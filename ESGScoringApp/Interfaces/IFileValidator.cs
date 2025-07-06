namespace ESGScoringApp.Interfaces;

public interface IFileValidator
{
    List<string> ValidateFilesExist(Dictionary<string, string> filePaths);
}