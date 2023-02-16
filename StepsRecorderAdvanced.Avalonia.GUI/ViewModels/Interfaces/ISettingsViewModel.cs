using System.ComponentModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Input;

namespace StepsRecorderAdvanced.Core.ViewModels.Interfaces
{
    public interface ISettingsViewModel
    {
        public bool RecordClick { get; set; }
        public bool RecordScroll { get; set; }
        public bool SettingVisible { get; set; }
        
        public DirectoryInfo TargetPath { get; }
        
        public IRelayCommand SelectTargetPathCommand { get; }
    }
}