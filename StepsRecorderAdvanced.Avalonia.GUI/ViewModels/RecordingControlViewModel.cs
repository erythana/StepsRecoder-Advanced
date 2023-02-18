using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Extensions;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Core.Models.Enums;
using Microsoft.Extensions.Configuration;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Core.Models.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class RecordingControlViewModel : ViewModelBase, IRecordingControlViewModel
    {
        private readonly ISharedSettings settings;
        private readonly IGUILog logger;
        private string currentBehaviourString = string.Empty;

        #region member fields

        private bool recordingActive;
        
        #endregion

        #region Constructor

        public RecordingControlViewModel(ISharedSettings settings, IGUILog logger)
        {
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
    }
}