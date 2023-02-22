using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Core.Models.Enums;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class RecordingControlViewModel : ViewModelBase, IRecordingControlViewModel, IMouseHookControl
    {
        private readonly IMouseHook mouseHook;
        private readonly ISharedSettings settings;
        private readonly IGUILog logger;

        #region member fields

        private bool recordingActive;
        
        #endregion

        #region Constructor

        public RecordingControlViewModel(IMouseHook mouseHook, ISharedSettings settings, IGUILog logger)
        {
            this.mouseHook = mouseHook;
            this.settings = settings;
            this.logger = logger;
            RecordingCommand = new RelayCommand(ExecuteRecordingCommand, CanExecuteRecordingCommand);
        }

        #endregion

        #region properties

        public string CurrentBehaviourString => recordingActive ? "Stop Recording" : "Start Recording";

        #endregion

        #region Commands

        public IRelayCommand RecordingCommand { get; }
        
        private void ExecuteRecordingCommand()
        {
            recordingActive = !recordingActive;
            logger.Log($"Recording state: {recordingActive}, settings used: {settings.WriteAsJSON()}", LogTypeEnum.Information);
            
            OnPropertyChanged(nameof(CurrentBehaviourString));
        }
        
        private bool CanExecuteRecordingCommand()
        {
            return true;
        }

        #endregion

        #region helper methods

  
        #endregion

        #region IMouseHookControl implementation

        public void RegisterHooks() => mouseHook.Install();
        
        public void DeregisterHooks() => mouseHook.Uninstall();

        #endregion

        
    }
}