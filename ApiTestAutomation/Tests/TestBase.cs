

using RestSharp;

public class TestBase : IClassFixture<ApiFixture>, IDisposable
{
    protected readonly RestClient _client;

    public TestBase(ApiFixture fixture)
    {
        _client = fixture.Client;
    }

    public void Dispose()
    {
        // No resources to dispose in the base class, but this method is here in case we need to add cleanup logic in the future.
    }
}