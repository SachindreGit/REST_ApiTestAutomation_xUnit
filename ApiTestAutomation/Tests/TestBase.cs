

using RestSharp;

public class TestBase : IClassFixture<ApiFixture>
{
    protected readonly RestClient _client;

    public TestBase(ApiFixture fixture)
    {
        _client = fixture.Client;
    }
}