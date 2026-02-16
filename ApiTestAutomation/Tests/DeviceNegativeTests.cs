using FluentAssertions;
using RestSharp;
using System.Net;

[Collection("Device Negative Tests")]
public class DeviceNegativeTests : IClassFixture<ApiFixture>
{
    private readonly RestClient _client;

    public DeviceNegativeTests(ApiFixture fixture)
    {
        _client = fixture.Client;
    }

   
    [Fact]
    public async Task AddDevice_ShouldReturnUnauthorized_WhenEndpointIsInvalid()
    {
        // Arrange - use invalid endpoint path
        var request = new RestRequest("/invalid-endpoint", Method.Post);
        request.AddJsonBody("invalid payload");

        // Act
        var response = await _client.ExecuteAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Content.Should().NotBeNullOrEmpty();

        response.Content.Should().Contain("Unauthorized path");
    }

    [Fact]
    public async Task AddDevice_ShouldReturnBadRequest_WhenPayloadIsNotValidJson()
    { 
        // Arrange - invalid device with wrong data types
        var request = new RestRequest("/objects", Method.Post);
        request.AddJsonBody("invalidDevice");

        // Act
        var response = await _client.ExecuteAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.Should().NotBeNullOrEmpty();
    }

    [Fact(Skip = "Backend allows device to be inserted - properties with wrong data types, to be fixed later. Because of this, the test will fail as it expects a BadRequest response.")]
    public async Task AddDevice_ShouldReturnBadRequest_WhenPayloadHasInvalidDataTypes()
    { 
        // Arrange - invalid device with wrong data types
        var invalidDevice = new
        {
            name = 123,
            data = new
            {
                year = "ssss",
                price = "s",
                cpuModel = 1,
                hardDiskSize = 3
            }
        };

        var request = new RestRequest("/objects", Method.Post);
        request.AddJsonBody(invalidDevice);

        // Act
        var response = await _client.ExecuteAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.Should().NotBeNullOrEmpty();
    }

    

}