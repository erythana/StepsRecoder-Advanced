using System.ComponentModel;
using StepsRecorderAdvanced.Core.Models.Enums;
using StepsRecorderAdvanced.Core.Models.Interfaces;

namespace StepsRecorder.Avalonia.GUI.Models.Interfaces
{
    public interface IGUILog : ILog, INotifyPropertyChanged
    {
        public string LoggedText { get; }
        
        public void ResetLoggedText();

        public void FilterByMinimal(LogTypeEnum logType);
    }
}