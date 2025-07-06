using CommandLine;
using ESGScoringApp;
using ESGScoringApp.Interfaces;
using ESGScoringApp.Models;
using ESGScoringApp.Operators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

ServiceProvider? serviceProvider = null;

try
{
    Parser.Default.ParseArguments<Options>(args)
        .WithParsed(options =>
        {
            serviceProvider = ConfigureServices();
            serviceProvider.GetRequiredService<ICommandlineProcessor>().Run(options);
        })
        .WithNotParsed(errs =>
            Console.WriteLine(
                $"Failed with parsing CL argument and errors are as follows:\n{string.Join("\n", errs)}"));
}
catch (Exception ex)
{
    if (serviceProvider != null)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Application failed");
    }
    else
        Console.WriteLine($"Application failed: {ex.Message}");
    Environment.Exit(1);
}
finally
{
    serviceProvider?.Dispose();
}

return;

static ServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    });

    services.AddScoped<ICommandlineProcessor, CommandlineProcessor>();
    services.AddScoped<IConfigurationMapper, ConfigurationMapper>();
    services.AddScoped<IDataSourceLoader, DataSourceLoader>();
    services.AddScoped<IFileValidator, FileValidator>();
    services.AddScoped<ICsvDataLoader, CsvDataLoader>();
    services.AddScoped<IYamlConfigLoader, YamlConfigLoader>();
    services.AddScoped<IScoringEngine, ScoringEngine>();
    services.AddScoped<ICsvWriterHelper,  CsvWriterHelper>();
    services.AddScoped<IExecuteOperator, ExecuteOperator>();
    
    services.AddScoped<IOperatorStrategy, SumOperatorStrategy>();
    services.AddScoped<IOperatorStrategy, DivideOperatorStrategy>();
    services.AddScoped<IOperatorStrategy, OrOperatorStrategy>();

    return services.BuildServiceProvider();
}