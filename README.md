# ESGScoringApp
ESGScoringApp is a command-line application for calculating ESG (Environmental, Social, Governance) scores for companies based on configurable metrics and multiple data sources. It parses a YAML configuration file, loads company data from CSV files, computes scores using configurable operations, and outputs results as a CSV.

## Features

- **Configurable Scoring**: Define metrics and operations in a YAML file.
- **Multiple Data Sources**: Supports emissions, waste, and disclosure data.
- **Extensible Operators**: Easily add new metric operations.
- **Dependency Injection**: Uses Microsoft.Extensions.DependencyInjection for modularity.
- **Logging**: Console logging for diagnostics.

## Project Structure

- `ESGScoringApp/`
  - `Program.cs`: Application entry point and DI setup.
  - `CommandlineProcessor.cs`: Orchestrates the workflow.
  - `ConfigurationMapper.cs`: Maps CLI options to file paths.
  - `DataSourceLoader.cs`: Validates and loads data/config files.
  - `CsvDataLoader.cs`: Loads and parses CSV data.
  - `YamlConfigLoader.cs`: Loads YAML config.
  - `ScoringEngine.cs`: Computes metrics for each company/year.
  - `CsvWriterHelper.cs`: Writes results to CSV.
  - `Operators/`: Operator strategies (sum, divide, or, etc).
  - `Models/`: Data models for config, metrics, and options.
  - `Interfaces/`: Service interfaces.
  - `Config/score_1.yaml`: Example scoring configuration.
  - `Data/`: Example data files.
  - `Results/`: Output directory for results.

## Software Requirements

- [.NET 9.0 SDK or later](https://dotnet.microsoft.com/download) (required to build and run the application)
- macOS, Linux, or Windows operating system
- (Optional) [Visual Studio Code](https://code.visualstudio.com/) or any C# compatible IDE for development

- Ensure the .NET CLI (`dotnet`) is available in your terminal by running:
   ```sh
    dotnet --version

## Usage

1. **Prepare Data & Config**
   - Place your data CSVs in `Data/` and your YAML config in `Config/`.

2. **Build the Application**
   ```sh
   dotnet build 

3. **Run the Application to view help menu**
   ```sh
   ./ESGSCoringApp -h
   
Pass appropriate options as shown in help menu for baseDirectoryPath to know the computed ESG Score

