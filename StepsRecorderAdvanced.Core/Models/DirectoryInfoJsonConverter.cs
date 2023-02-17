using System.Text.Json;
using System.Text.Json.Serialization;

namespace StepsRecorderAdvanced.Core.Models;

public class DirectoryInfoJsonConverter : JsonConverter<DirectoryInfo>
{
    public override DirectoryInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        new DirectoryInfo(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.FullName);
}