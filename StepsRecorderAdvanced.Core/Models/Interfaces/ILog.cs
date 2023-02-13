using StepsRecorderAdvanced.Core.Models.Enums;

namespace StepsRecorderAdvanced.Core.Models.Interfaces
{
    public interface ILog
    {
        void Log(string message, LogTypeEnum logType);
    }
}