namespace ESGScoringApp.Interfaces;

public interface IOperatorStrategy
{
    string Name { get; } 
    double? Evaluate(List<double?> parameters);
}