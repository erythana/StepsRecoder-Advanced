using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces
{
    public interface IMainWindowViewModel
    {
        public IRecordingControlViewModel RecordingControlViewModel { get; }
        
        public ISettingsViewModel SettingsViewModel { get; }
        
        public ILoggingViewModel LoggingViewModel { get; }
    }
}
