using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using StepsRecorder.Avalonia.GUI.Models;
using StepsRecorder.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Extensions;
using StepsRecorderAdvanced.Core.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorder.Avalonia.GUI.Models.Interfaces;
using StepsRecorder.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Core.Models.Enums;
using Microsoft.Extensions.Configuration;

namespace StepsRecorder.Avalonia.GUI.ViewModels
{
    public class LoggingViewModel : ViewModelBase, ILoggingViewModel
    {
        #region member fields

        private readonly IGUILog logger;
        private bool loggingVisible;
        private LogTypeEnum selectedLogType;

        #endregion

        #region Constructor

        public LoggingViewModel(IConfiguration configuration, IGUILog logger)
        {
            this.logger = logger;
            this.logger.PropertyChanged += (_, _) => PropagateLoggingUpdated();

            AvailableLogTypes = Enum.GetValues<LogTypeEnum>()
                .Where<LogTypeEnum>(x => System.Diagnostics.Debugger.IsAttached || x != LogTypeEnum.Debug).ToArray();
            var defaultLogLevel = configuration.GetValue<string>("Logging:LogLevel:Default");
            SelectedLogType = Enum.TryParse<LogTypeEnum>(defaultLogLevel, out var parsedLogType)
                ? parsedLogType
                : LogTypeEnum.Information;

            ExportLogCommand = new RelayCommand(ExecuteExportLog, CanExecuteExportLog);
            ClearLogCommand = new RelayCommand(ExecuteClearLog, CanExecuteClearLog);

            LogTypeEnumColor = logType =>
                logType switch
                {
                    LogTypeEnum.Debug => ApplicationConstants.Logging.DebugBrush,
                    LogTypeEnum.Warning => ApplicationConstants.Logging.WarningBrush,
                    LogTypeEnum.Error => ApplicationConstants.Logging.ErrorBrush,
                    _ => ApplicationConstants.Logging.InformationBrush,
                };

            logger.Log("abc", LogTypeEnum.Debug);
            logger.Log("abc", LogTypeEnum.Warning);
            logger.Log("abc", LogTypeEnum.Error);
            logger.Log("abc", LogTypeEnum.Information);
        }

        #endregion

        #region properties

        public LogTypeEnum[] AvailableLogTypes { get; }

        public LogTypeEnum SelectedLogType
        {
            get => selectedLogType;
            set
            {
                SetProperty(ref selectedLogType, value);
                logger.FilterByMinimal(selectedLogType);
            }
        }

        public Func<LogTypeEnum, ISolidColorBrush> LogTypeEnumColor { get; }

        public bool LoggingVisible
        {
            get => loggingVisible;
            set => SetProperty(ref loggingVisible, value);
        }

        public string LoggedText => logger.LoggedText;

        #endregion

        #region Commands

        public IRelayCommand ExportLogCommand { get; }

        public IRelayCommand ClearLogCommand { get; }

        private void ExecuteClearLog() =>
            logger.ResetLoggedText();

        private bool CanExecuteClearLog() =>
            CanExecuteSaveClearLog();

        private async void ExecuteExportLog()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime
                {
                    MainWindow: { } mainWindow
                })
                return;

            var defaultLogFileName =
                $"{ApplicationConstants.ApplicationName}-{DateTime.Now:MM/dd/yyyy}".SanitizeFilename();

            var saveDialog = CreateSaveLogFileDialog(defaultLogFileName);
            if (await saveDialog?.ShowAsync(mainWindow)! is { } path)
                await WriteLogFile(path, LoggedText);
        }

        private bool CanExecuteExportLog() =>
            CanExecuteSaveClearLog();

        #endregion

        #region helper methods

        private bool CanExecuteSaveClearLog() => !string.IsNullOrWhiteSpace(LoggedText);

        private void PropagateLoggingUpdated()
        {
            OnPropertyChanged(nameof(LoggedText));
            ExportLogCommand.NotifyCanExecuteChanged();
            ClearLogCommand.NotifyCanExecuteChanged();
        }

        private static async Task WriteLogFile(string filePath, string textToWrite)
        {
            await File.WriteAllTextAsync(filePath, textToWrite);
        }

        private static SaveFileDialog? CreateSaveLogFileDialog(string fileName) =>
            new()
            {
                Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments,
                    Environment.SpecialFolderOption.Create),
                Title = "Save log to directory",
                InitialFileName = fileName,
                Filters = new List<FileDialogFilter>
                {
                    new()
                        { Name = "Log", Extensions = new List<string> { "log" } },
                    new()
                        { Name = "Txt", Extensions = new List<string> { "txt" } }
                },
                DefaultExtension = "log"
            };

        #endregion
    }
}