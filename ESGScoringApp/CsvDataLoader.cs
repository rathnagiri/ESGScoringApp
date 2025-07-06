using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ESGScoringApp.Interfaces;

namespace ESGScoringApp;

public class CsvDataLoader : ICsvDataLoader
{
    public Dictionary<(string, int), Dictionary<string, double?>> Load(string filePath,
        string prefix)
    {
        var result = new Dictionary<(string, int), Dictionary<string, double?>>();
        var latestDates = new Dictionary<(string, int), DateTime>();

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<dynamic>().ToList();

        foreach (var record in records)
        {
            var dict = (IDictionary<string, object>)record;

            var companyId = dict["company_id"].ToString();
            int year;
            DateTime? date = null;

            if (dict.ContainsKey("date") && DateTime.TryParse(dict["date"]?.ToString(), out var parsedDate))
            {
                date = parsedDate;
                year = date.Value.Year;
            }
            else if (dict.ContainsKey("year") && int.TryParse(dict["year"]?.ToString(), out var parsedYear))
            {
                year = parsedYear;
            }
            else
            {
                throw new InvalidDataException("CSV row is missing either a valid 'date' or 'year' column.");
            }

            var key = (companyId, year);

            var shouldReplace =
                date.HasValue && (!latestDates.ContainsKey(key) || latestDates[key] < date.Value) ||
                !date.HasValue && !result.ContainsKey(key); // when no date, just keep latest row by order

            if (shouldReplace)
            {
                var values = new Dictionary<string, double?>();

                foreach (var kv in dict)
                {
                    if (kv.Key == "company_id" || kv.Key == "date" || kv.Key == "year") continue;

                    if (double.TryParse(kv.Value?.ToString(), out var val))
                        values[$"{prefix}.{kv.Key}"] = val;
                    else
                        values[$"{prefix}.{kv.Key}"] = null;
                }

                result[key] = values;

                if (date.HasValue)
                    latestDates[key] = date.Value;
            }
        }

        return result;
    }
}