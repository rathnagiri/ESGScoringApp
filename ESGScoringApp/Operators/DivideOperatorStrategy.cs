using ESGScoringApp.Interfaces;

namespace ESGScoringApp.Operators;

public class DivideOperatorStrategy : IOperatorStrategy
{
    public string Name => "divide";

    public double? Evaluate(List<double?> parameters)
    {
        if (parameters.Count < 2 || parameters[1] == 0 || parameters[1] == null)
            return null;

        return parameters[0] / parameters[1];
    }
}