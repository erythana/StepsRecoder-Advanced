using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.Configuration;
using StepsRecorderAdvanced.Core.Models.Extensions;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models;
using StepsRecorderAdvanced.Core.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly FileInfo settingsFile;
        private readonly ISharedSettingReaderWriter settingsReaderWriter;
        private readonly Dictionary<string, List<string>> propertyErrors = new();
        
        private bool settingVisible;
        private ISharedSettings settings;
        
        private DirectoryInfo screenshotTargetPath;

        #region member fields

        #endregion

        #region Constructor

        public SettingsViewModel(IConfiguration configuration, ISharedSettingReaderWriter settingsReaderWriter, ISharedSettings settings)
        {
            this.settings = settings;
            this.settingsReaderWriter = settingsReaderWriter;

            var settingsDirectory = Environment.ExpandEnvironmentVariables(configuration.GetValue<string>("ConfigDirectory")
                                                                           ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            settingsFile = new FileInfo(Path.Combine(settingsDirectory, ApplicationConstants.SettingFileName));
            
            SelectTargetPathCommand = new RelayCommand(ExecuteSelectTargetPathCommand);
        }

        private async void ExecuteSelectTargetPathCommand()
        {
            var folderDialog = new OpenFolderDialog
            {
                Title = "Choose directory for screenshots",
                Directory = Environment.SpecialFolder.MyPictures.ToString()
            };

            var result = await folderDialog.ShowAsync(App.GetMainWindow());
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

        public DirectoryInfo ScreenshotTargetPath
        {
            get => settings.TargetDirectory;
            private set
            {
                settings.TargetDirectory = value;
                
                ClearErrors(nameof(ScreenshotTargetPath));
                if (!ValidateTargetPath(value, out var errorMessage))
                    AddError(nameof(ScreenshotTargetPath), errorMessage);

                SetProperty(ref screenshotTargetPath, value);
            }
        }

        public bool SettingVisible
        {
            get => settingVisible;
            set => SetProperty(ref settingVisible, value);
        }

        #region ILoadWriteSettings implementation

        public async Task LoadSettings()
        {
            var sharedSettings = await settingsReaderWriter.Read(settingsFile);
            if (sharedSettings is not null)
                settings = sharedSettings;
            
            OnPropertiesChanged(nameof(ScreenshotTargetPath), nameof(RecordScroll), nameof(RecordClick));
        }

        public async Task SaveSettings()
        {
            await settingsReaderWriter.Write(settingsFile, settings.WriteAsJSON());
        }

        #endregion

        #endregion

        #region Commands

        public IRelayCommand SelectTargetPathCommand { get; }

        #endregion

        #region helper methods

        private void TrySetTargetDirectory(string? result)
        {
            if (result is null) return;
            ScreenshotTargetPath = new DirectoryInfo(result);
        }
        
        private static bool ValidateTargetPath(DirectoryInfo target, out string errorMessage)
        {
            errorMessage = target switch
            {
                { Exists: false } => "The specified path doesn't exist",
                { } when !target.HasWriteAccess() => "Can't write to directory",
                _ => string.Empty
            };
            return string.IsNullOrEmpty(errorMessage);
        }

        #endregion

        #region INotifyDataError implementation

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName) =>
            propertyName is not null && propertyErrors.TryGetValue(propertyName, out var errors)
                ? errors
                : Enumerable.Empty<object>();

        public bool HasErrors => propertyErrors.Count > 0;

        private void AddError(string propertyName, string error)
        {
            if (!propertyErrors.TryAdd(propertyName, new List<string> { error }) &&
                !propertyErrors[propertyName].Contains(error))
                propertyErrors[propertyName].Add(error);


            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            if (!propertyErrors.ContainsKey(propertyName)) return;

            propertyErrors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #endregion
    }
}