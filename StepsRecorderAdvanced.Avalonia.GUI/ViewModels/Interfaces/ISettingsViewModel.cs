using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Input;
using StepsRecorderAdvanced.Core.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces
{
    public interface ISettingsViewModel : INotifyDataErrorInfo , ILoadWriteSettings
    {
        public bool RecordClick { get; set; }
        public bool RecordScroll { get; set; }
        public bool SettingVisible { get; set; }
        
        public DirectoryInfo ScreenshotTargetPath { get; }
        
        public IRelayCommand SelectTargetPathCommand { get; }
        
        public IEnumerable<IScreen> AvailableScreens { get; }
        
        public IEnumerable<IScreen> SelectedScreens { get; }
    }

}