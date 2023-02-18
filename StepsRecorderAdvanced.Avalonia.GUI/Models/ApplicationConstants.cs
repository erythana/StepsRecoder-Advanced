using System;
using Avalonia.Media;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models
{
    internal static class ApplicationConstants
    {
        public const string ApplicationName = "StepsRecorder-Advanced";
        public const string SettingFileName = "config.json";

        public static class Logging
        {
            public static readonly ISolidColorBrush DebugBrush = Brushes.Green;
            public static readonly ISolidColorBrush InformationBrush = Brushes.Black; //TODO: Check if this is okay for all themes - do we need something for dark theme?
            public static readonly ISolidColorBrush WarningBrush = Brushes.Orange;
            public static readonly ISolidColorBrush ErrorBrush = Brushes.Red;
        }
    }
}
