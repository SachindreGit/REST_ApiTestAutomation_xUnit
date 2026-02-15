using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class UnixTimestampConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new JsonException("Expected a number for Unix timestamp");

        long unixTimestampMs = reader.GetInt64();
        return DateTime.UnixEpoch.AddMilliseconds(unixTimestampMs);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        long unixTimestampMs = (long)(value - DateTime.UnixEpoch).TotalMilliseconds;
        writer.WriteNumberValue(unixTimestampMs);
    }
}