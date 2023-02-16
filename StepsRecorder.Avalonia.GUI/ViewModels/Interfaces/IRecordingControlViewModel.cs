using Microsoft.Toolkit.Mvvm.Input;

namespace StepsRecorderAdvanced.Core.ViewModels.Interfaces
{
    public interface IRecordingControlViewModel
    {
        public string CurrentBehaviourString { get; }
        
        public IRelayCommand RecordingCommand { get; }
    }
}