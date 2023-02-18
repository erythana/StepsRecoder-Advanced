namespace StepsRecorderAdvanced.Core.Models.Interfaces
{
    public interface ILoadWriteSettings
    {
        public Task LoadSettings();

        public Task SaveSettings();
    }
}