using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using StepsRecorderAdvanced.Core.Models.Extensions;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Core.Models.Interfaces;
using StepsRecorderAdvanced.Core.ViewModels.Interfaces;

namespace StepsRecorder.Avalonia.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly ISharedSettings settings;
        private bool settingVisible;
        private DirectoryInfo targetPath;

        #region member fields

        #endregion

        #region Constructor

        public SettingsViewModel(ISharedSettings settings)
        {
            //TODO: Load TargetPath from settings
            this.settings = settings;
            SelectTargetPathCommand = new RelayCommand(ExecuteSelectTargetPathCommand);
        }

        private async void ExecuteSelectTargetPathCommand()
        {
            var folderDialog = new OpenFolderDialog
            {
                Title = "Choose directory for screenshots",
                Directory = Environment.SpecialFolder.MyPictures.ToString()
            };

            var result = await folderDialog.ShowAsync(GetMainWindow()!); //TODO: check how to solve this
            TrySetTargetDirectory(result);
        }

        #endregion

        #region properties

        public bool RecordClick
        {
            get => settings.RecordClick;
            set => settings.RecordClick = value;
        }

        public bool RecordScroll
        {
            get => settings.RecordScroll;
            set => settings.RecordScroll = value;
        }

        public DirectoryInfo TargetPath
        {
            get => targetPath;
            private set
            {
                SetProperty(ref targetPath, value);
                settings.TargetDirectory = value;
            }
        }

        public bool SettingVisible
        {
            get => settingVisible;
            set => SetProperty(ref settingVisible, value);
        }

        #endregion

        #region Commands

        public IRelayCommand SelectTargetPathCommand { get; }

        #endregion

        #region helper methods

        private void TrySetTargetDirectory(string? result)
        {
            if (result is not null)
            {
                var directoryInfo = new DirectoryInfo(result);
                if (directoryInfo.HasWriteAccess())
                    TargetPath = directoryInfo;
            }
            //TODO: Proper handling, maybe with an validationerror or some sort of notificatino for the user
        }
        
        private Window? GetMainWindow() =>
            Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime
            {
                MainWindow: { } mainWindow
            }
                ? mainWindow
                : throw new NotSupportedException("This application needs to run in a desktop environment.");

        #endregion
    }
}