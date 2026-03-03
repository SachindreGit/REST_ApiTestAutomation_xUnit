

using Microsoft.Extensions.Configuration;

public class ConfigurationFile
{
    public string BaseUrl { get; }
    public int TimeoutSeconds { get; }

    public ConfigurationFile()
    {  
        
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");        
        if(environmentName != null)
        {
            configBuilder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
        }
        var configuration = configBuilder.Build();

        var section = configuration.GetSection("ApiSettings");

        BaseUrl = section["BaseUrl"] ?? "";
        TimeoutSeconds = int.Parse(section["TimeoutSeconds"] ?? "0");

    }


}