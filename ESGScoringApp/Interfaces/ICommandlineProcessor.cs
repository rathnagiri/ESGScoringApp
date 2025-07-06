using ESGScoringApp.Models;

namespace ESGScoringApp.Interfaces;

public interface ICommandlineProcessor
{
    void Run(Options options);
}