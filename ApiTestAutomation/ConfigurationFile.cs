

using Microsoft.Extensions.Configuration;

public class ConfigurationFile
{
    public string BaseUrl { get; }
    public int TimeoutSeconds { get; }

    public ConfigurationFile()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .Build();

        var section = configuration.GetSection("ApiSettings");

        BaseUrl = section["BaseUrl"] ?? "";
        TimeoutSeconds = int.Parse(section["TimeoutSeconds"] ?? "0");

    }


}