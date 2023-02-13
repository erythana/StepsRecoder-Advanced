using System;
using System.Collections.Generic;
using Avalonia.Media;
using StepsRecorder.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Core.Models.Enums;
using Microsoft.Toolkit.Mvvm.Input;

namespace StepsRecorder.Avalonia.GUI.ViewModels.Interfaces
{
    public interface ILoggingViewModel
    {
        public LogTypeEnum[] AvailableLogTypes { get; }
        
        public LogTypeEnum SelectedLogType { get; set; }
        
        public bool LoggingVisible { get; set; }

        public Func<LogTypeEnum, ISolidColorBrush> LogTypeEnumColor { get; }
        
        public IRelayCommand ExportLogCommand { get; }
        
        public IRelayCommand ClearLogCommand { get; }
    }
}