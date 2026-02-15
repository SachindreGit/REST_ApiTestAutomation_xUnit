using FluentAssertions;
using RestSharp;
using System.Net;
using System.Text.Json;


[Collection("Device Positive Tests")]
public class DevicePositiveTests : IClassFixture<ApiFixture>
{
    private readonly RestClient _client;

    public DevicePositiveTests(ApiFixture fixture)
    {
        _client = fixture.Client;
    }

    private async Task<Device> CreateDeviceAsync(Device deviceData)
    {
        var request = new RestRequest("/objects", Method.Post);
        request.AddJsonBody(deviceData);

        var response = await _client.ExecuteAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNullOrEmpty();

        return JsonSerializer.Deserialize<Device>(response.Content);
    }

    [Fact]
    public async Task GetListOfDevices_ShouldReturnCorrectCount()
    {
        var request = new RestRequest("/objects", Method.Get);

        var response = await _client.ExecuteAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNullOrEmpty();

        var items = JsonSerializer.Deserialize<List<object>>(response.Content);
        int count = items?.Count ?? 0;
        Assert.Equal(13, count);
    }

    [Fact]
    public async Task AddDevice_ShouldCreateDeviceSuccessfully()
    {
        var deviceData = new Device
        {
            name = $"Apple MacBook - {Guid.NewGuid()}",
            data = new DeviceData
            {
                year=2019,
                price=1849.99,
                cpuModel="Intel Core i9",
                hardDiskSize="1 TB"
            }
        };

        var item = await CreateDeviceAsync(deviceData);
        Assert.NotNull(item);
        Assert.NotNull(item.id);
        Assert.Equal(deviceData.name, item.name);

    }

    [Fact]
    public async Task GetDevice_ShouldReturnDeviceSuccessfully()
    {
        var deviceData = new Device
        {
            name = $"Apple MacBook - {Guid.NewGuid()}",
            data = new DeviceData
            {
                year = 2019,
                price = 1849.99,
                cpuModel = "Intel Core i9",
                hardDiskSize = "1 TB"
            }
        };

        var createdItem = await CreateDeviceAsync(deviceData);

        var request = new RestRequest($"/objects/{createdItem.id}", Method.Get);
        var response = await _client.ExecuteAsync(request);

        response.Content.Should().NotBeNullOrEmpty();
        var retrievedItem = JsonSerializer.Deserialize<Device>(response.Content);

        Assert.NotNull(retrievedItem);
        Assert.Equal(createdItem.id, retrievedItem.id);
    }

    [Fact]
    public async Task UpdateDevice_ShouldUpdateDeviceSuccessfully()
    {
        // Prepare initial data
        var deviceData = new Device
        {
            name = $"Apple MacBook - {Guid.NewGuid()}",
            data = new DeviceData
            {
                year = 2019,
                price = 1849.99,
                cpuModel = "Intel Core i9",
                hardDiskSize = "1 TB"
            }
        };

        var createdItem = await CreateDeviceAsync(deviceData);

        // Prepare updated data
        var updatedDevice = new Device
        {
            name = "Updated MacBook Pro 16",
            data = new DeviceData
            {
                year = 2022,
                price = 2199.99,
                cpuModel = "Apple M2",
                hardDiskSize = "2 TB"
            }
        };

        var updateRequest = new RestRequest($"/objects/{createdItem.id}", Method.Put);
        updateRequest.AddJsonBody(updatedDevice);

        // Act
        var response = await _client.ExecuteAsync(updateRequest);

        // Assert response
        response.Content.Should().NotBeNullOrEmpty();
        var retrievedItem = JsonSerializer.Deserialize<Device>(response.Content);

        Assert.NotNull(retrievedItem);
        Assert.Equal(createdItem.id, retrievedItem.id);
        Assert.Equal(updatedDevice.name, retrievedItem.name);
        Assert.Equal(updatedDevice.data.year, retrievedItem.data.year);
        Assert.Equal(updatedDevice.data.price, retrievedItem.data.price);
        Assert.Equal(updatedDevice.data.cpuModel, retrievedItem.data.cpuModel);
        Assert.Equal(updatedDevice.data.hardDiskSize, retrievedItem.data.hardDiskSize);
    }

    [Fact]
    public async Task DeleteDevice_ShouldRemoveDeviceSuccessfully()
    {
        // Arrange - create device first
        var deviceData = new Device
        {
            name = $"MacBook-{Guid.NewGuid()}",
            data = new DeviceData
            {
                year = 2019,
                price = 1849.99,
                cpuModel = "Intel Core i9",
                hardDiskSize = "1 TB"
            }
        };

        var createdItem = await CreateDeviceAsync(deviceData);

        // Act - delete the object
        var deleteRequest = new RestRequest($"/objects/{createdItem.id}", Method.Delete);
        var deleteResponse = await _client.ExecuteAsync(deleteRequest);

        // Assert delete response
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteResponse.Content.Should().NotBeNullOrEmpty();

        // Deserialize response
        var deleteResult = JsonSerializer.Deserialize<DeleteDeviceResponse>(deleteResponse.Content!);

        deleteResult.Should().NotBeNull();
        deleteResult.message.Should().Contain(createdItem.id);
        deleteResult.message.Should().Be($"Object with id = {createdItem.id} has been deleted.");

    }
   


}