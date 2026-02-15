using System.Text.Json.Serialization;

public class Device
{
    public String id {get;set;}
    public String name {get;set;}
    public DeviceData data {get;set;}

    // Unix timestamp in milliseconds is converted to 
    // DateTime using the custom converter
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime createdAt {get;set;}
}