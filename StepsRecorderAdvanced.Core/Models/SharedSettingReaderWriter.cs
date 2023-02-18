namespace StepsRecorderAdvanced.Core.Models.Interfaces;

public class SharedSettingReaderWriter<T> : ISharedSettingReaderWriter<T> where T : ISharedSettings
{
    public T Read(string settings)
    {
        throw new NotImplementedException();
    }

    public Task Write(T settings)
    {
        throw new NotImplementedException();
    }
}