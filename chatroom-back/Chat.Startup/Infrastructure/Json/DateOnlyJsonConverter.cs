using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chat.Startup.Infrastructure.Json;

/// <inheritdoc />
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    /// <inheritdoc />
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Is type nullable?
        if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>) && reader.TokenType is JsonTokenType.Null)
        {
            return default;
        }

        string? dateStr = reader.GetString();
        
        if (dateStr is not null && DateOnly.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
        {
            return date;
        }

        throw new JsonException($"Invalid date format: {dateStr}");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O"));
    }
}