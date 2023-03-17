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
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class RecordingControlViewModel : ViewModelBase, IRecordingControlViewModel
    {
        #region member fields
        
        private readonly IXPlatformScreenshotUtilityFactory screenshotUtilityFactory;
        private IScreenshotUtility? screenshotUtility;
        
        private readonly IMouseHook mouseHook;
        private readonly ISharedSettings settings;
        private readonly IGUILog logger;
        private string currentBehaviourString = string.Empty;
        
        private bool recordingActive;
        
        #endregion
        

        #region Constructor

        public RecordingControlViewModel(IXPlatformScreenshotUtilityFactory screenshotUtilityFactory, IMouseHook mouseHook, ISharedSettings settings, IGUILog logger)
        {
            this.screenshotUtilityFactory = screenshotUtilityFactory;
            this.mouseHook = mouseHook;
            this.settings = settings;
            this.logger = logger;
            RecordingCommand = new RelayCommand(ExecuteRecordingCommand, CanExecuteRecordingCommand);
        }

        #endregion

        #region properties

        public string CurrentBehaviourString {
            get => recordingActive
                ? "Stop Recording"
                : "Start Recording";
        }

        #endregion

        #region Commands

        public IRelayCommand RecordingCommand { get; }
        
        private void ExecuteRecordingCommand()
        {
            recordingActive = !recordingActive;
            if(recordingActive)
                InitializeRecording();
            else
                ShutdownRecording();
            
            logger.Log($"Recording state: {recordingActive}, settings used: {settings.WriteAsJSON()}", LogTypeEnum.Information);
            
            OnPropertyChanged(nameof(CurrentBehaviourString));
        }
        
        private bool CanExecuteRecordingCommand()
        {
            return true;
        }

        #endregion

        #region helper methods

        private void InitializeRecording()
        {
            screenshotUtility = screenshotUtilityFactory.Create();
            mouseHook.Install();
        }

        private void ShutdownRecording()
        {
            mouseHook.Uninstall();
        }

        #endregion
        
        
    }
}