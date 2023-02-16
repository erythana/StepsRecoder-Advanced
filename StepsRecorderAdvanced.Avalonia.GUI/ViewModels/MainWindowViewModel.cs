using System.Collections.Specialized;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region Constructor

        public MainWindowViewModel(IRecordingControlViewModel recordingControlViewModel, ISettingsViewModel settingsViewModel, ILoggingViewModel loggingViewModel)
        {
            RecordingControlViewModel = recordingControlViewModel;
            SettingsViewModel = settingsViewModel;
            LoggingViewModel = loggingViewModel;
        }

        #endregion

        #region Commands

        #endregion

        #region Properties

        public IRecordingControlViewModel RecordingControlViewModel { get; private set; }
        
        public ISettingsViewModel SettingsViewModel { get; private set; }
        
        public ILoggingViewModel LoggingViewModel { get; private set; }

        #endregion

        #region helper methods

        #endregion
    }
}