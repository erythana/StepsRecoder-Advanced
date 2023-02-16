using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorder.Avalonia.GUI.ViewModels.Interfaces
{
    public interface IMainWindowViewModel
    {
        public IRecordingControlViewModel RecordingControlViewModel { get; }
        
        public ISettingsViewModel SettingsViewModel { get; }
        
        public ILoggingViewModel LoggingViewModel { get; }
    }
}
