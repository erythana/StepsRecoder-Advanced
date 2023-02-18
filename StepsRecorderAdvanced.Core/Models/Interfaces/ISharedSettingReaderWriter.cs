namespace StepsRecorderAdvanced.Core.Models.Interfaces;

public interface ISharedSettingReaderWriter<T> where T : ISharedSettings
{
    public T Read(string settings);
    public Task Write(T settings);
}