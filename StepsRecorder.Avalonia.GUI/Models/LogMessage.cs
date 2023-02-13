using System;
using StepsRecorderAdvanced.Core.Models.Enums;

namespace StepsRecorder.Avalonia.GUI.Models
{
    public partial class GUILogger
    {
        private readonly struct LogMessage
        {
            #region member fields

            private readonly string message;

            #endregion

            #region Constructor

            public LogMessage(string message, LogTypeEnum type)
            {
                this.message = FormatLoggedMessage(message, type);
                LogMessageType = type;
            }

            #endregion

            #region Properties

            public LogTypeEnum LogMessageType { get; }

            #endregion

            #region overrides

            public override string ToString() => message;

            #endregion

            #region Helper Methods

            private static string FormatLoggedMessage(string message, LogTypeEnum type) =>
                $"{type} -- {DateTime.Now}: {message}\r\n";

            #endregion
        }
    }
}