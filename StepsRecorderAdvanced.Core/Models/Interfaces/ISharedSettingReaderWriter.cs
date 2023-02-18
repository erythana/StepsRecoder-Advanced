namespace StepsRecorderAdvanced.Core.Models.Interfaces;

public interface ISharedSettingReaderWriter
{
    public Task<SharedSettings?> Read(FileInfo settingsFile);
    public Task Write(FileInfo settingsFile, string content);
}