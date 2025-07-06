using ESGScoringApp.Interfaces;
using Microsoft.Extensions.Logging;

namespace ESGScoringApp;

public class FileValidator(ILogger<FileValidator> logger) : IFileValidator
{
    public List<string> ValidateFilesExist(Dictionary<string, string> filePaths)
    {
        var errors = new List<string>();

        logger.LogInformation("Validating {KeysCount} files existence in file system", filePaths.Keys.Count);
        foreach (var kvp in filePaths)
        {
            var fileType = kvp.Key;
            var filePath = kvp.Value;

            if (!File.Exists(filePath))
            {
                var error = $"File for '{fileType}' does not exist: '{filePath}'";
                errors.Add(error);
                logger.LogError(error);
            }
            else
            {
                logger.LogInformation($"File at location: {filePath} validated successfully");
            }
        }

        return errors;
    }
}