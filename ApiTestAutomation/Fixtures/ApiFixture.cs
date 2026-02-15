
using RestSharp;

public class ApiFixture : IDisposable
{
    public RestClient Client { get; }

    public ApiFixture()
    {
        ConfigurationFile configurationFile = new ConfigurationFile();

        var options = new RestClientOptions(configurationFile.BaseUrl)
        {
            Timeout = TimeSpan.FromSeconds(configurationFile.TimeoutSeconds)
        };

        Client = new RestClient(options);
    }

    public void Dispose()
    {
        Client?.Dispose();
    }
}
