using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StepsRecorderAdvanced.Core.Models.Enums;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models
{
    public partial class GUILogger : ObservableObject, IGUILog
    {
        #region member fields

        private LogTypeEnum minimalLogLevel;
        private readonly IList<LogMessage> loggedMessages;
        private string loggedText = string.Empty;

        #endregion

        #region constructor

        public GUILogger()
        {
            loggedMessages = new List<LogMessage>();
        }

        #endregion
        
        #region Properties
        
        public string LoggedText 
        { 
            get => loggedText;
            private set => SetProperty(ref loggedText, value); 
        }
        

        #endregion

        public void Log(string message, LogTypeEnum logType)
        {
            var logEntry = new LogMessage(message, logType);
            loggedMessages.Add(logEntry);

            FilterByMinimal(minimalLogLevel);
        }
        
        public void ResetLoggedText()
        {
            loggedMessages.Clear();
            LoggedText = string.Empty;
        }

        public void FilterByMinimal(LogTypeEnum logType)
        {
            minimalLogLevel = logType;
            
            var relevantMessages = loggedMessages.Where(m => m.LogMessageType >= minimalLogLevel);
            LoggedText = relevantMessages.Aggregate(new StringBuilder(), (sb, message) => sb.Append(message.ToString()),
                x => x.ToString());
        }
    }
}
