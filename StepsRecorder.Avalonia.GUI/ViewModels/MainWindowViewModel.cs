using System.Collections.Specialized;
using StepsRecorder.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorder.Avalonia.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region Constructor

        public MainWindowViewModel(IRecordingControlViewModel recordingControlViewModel, ISettingViewModel settingViewModel, ILoggingViewModel loggingViewModel)
        {
            RecordingControlViewModel = recordingControlViewModel;
            SettingViewModel = settingViewModel;
            LoggingViewModel = loggingViewModel;
        }

        #endregion

        #region Commands

        #endregion

        #region Properties

        public IRecordingControlViewModel RecordingControlViewModel { get; private set; }
        
        public ISettingViewModel SettingViewModel { get; private set; }
        
        public ILoggingViewModel LoggingViewModel { get; private set; }

        #endregion

        #region helper methods

        #endregion
    }
}