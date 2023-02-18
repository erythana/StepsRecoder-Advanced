using System.Text.Json;
using StepsRecorderAdvanced.Core.Models.Extensions;

namespace StepsRecorderAdvanced.Core.Models.Interfaces;

public class SharedSettingReaderWriter : ISharedSettingReaderWriter
{
    public async Task<SharedSettings?> Read(FileInfo settingsFile)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new DirectoryInfoJsonConverter()
                }
            };
            await using var stream = settingsFile.OpenRead();
            return await JsonSerializer.DeserializeAsync<SharedSettings>(stream, options);
        }
        catch
        {
            return new SharedSettings();
        }

    }

    public async Task Write(FileInfo settingsFile, string content)
    {
        try
        {
            if (!settingsFile.Directory?.Exists == true)
                Directory.CreateDirectory(settingsFile.Directory!.FullName);

            if (!settingsFile.Directory?.HasWriteAccess() == true || settingsFile.Exists && settingsFile.IsReadOnly)
                return;

            await using var stream = settingsFile.CreateText();
            await stream.WriteAsync(content);
            await stream.FlushAsync();
        }
        catch (Exception ex)
        {
            
        }
    }
}