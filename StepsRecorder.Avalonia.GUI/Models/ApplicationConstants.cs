using System;
using Avalonia.Media;

namespace StepsRecorder.Avalonia.GUI.Models
{
    internal static class ApplicationConstants
    {
        public static readonly string ApplicationName = "StepsRecorder";
        public static readonly string SettingFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\settings.json";//Seperate file or appsettings.json?

        public static class Logging
        {
            public static readonly ISolidColorBrush DebugBrush = Brushes.Green;
            public static readonly ISolidColorBrush InformationBrush = Brushes.Black; //TODO: Check if this is okay for all themes - do we need something for dark theme?
            public static readonly ISolidColorBrush WarningBrush = Brushes.Orange;
            public static readonly ISolidColorBrush ErrorBrush = Brushes.Red;
        }
    }
}
