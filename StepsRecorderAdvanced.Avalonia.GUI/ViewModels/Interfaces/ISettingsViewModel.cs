﻿using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces
{
    public interface ISettingsViewModel : INotifyDataErrorInfo 
    {
        public bool RecordClick { get; set; }
        public bool RecordScroll { get; set; }
        public bool SettingVisible { get; set; }
        
        public DirectoryInfo ScreenshotTargetPath { get; }
        
        public IRelayCommand SelectTargetPathCommand { get; }

        public Task LoadSettings();

        public Task SaveSettings();
    }
}