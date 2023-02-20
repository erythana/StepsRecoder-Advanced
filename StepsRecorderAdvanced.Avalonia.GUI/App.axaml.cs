using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Avalonia.GUI.Views;
using StepsRecorderAdvanced.Core.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                var dataContext = desktop.MainWindow.DataContext;
                desktop.Startup += async (_, _) =>
                    await StartupProcedures(dataContext);
                desktop.Exit += async (_, _) =>
                    await ExitProcedures(dataContext);
            }
                
            
            base.OnFrameworkInitializationCompleted();
        }
        
        public static Window GetMainWindow() =>
            Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime
            {
                MainWindow: { } mainWindow
            }
                ? mainWindow
                : throw new NotSupportedException("This application needs to run in a desktop environment.");

        #region handler delegation
        
        private async Task StartupProcedures(object? dataContext)
        {
            await TryLoadSettings(dataContext);
            TryRegisterMouseHooks(dataContext);
        }

        private async Task ExitProcedures(object? dataContext)
        {
            await TrySaveSettings(dataContext);
            TryUnregisterMouseHooks(dataContext);
        }

        #endregion
        
        #region helper methods
        
        private async static Task TrySaveSettings(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { SettingsViewModel: ILoadWriteSettings settingsViewModel })
                await settingsViewModel.SaveSettings();
        }
        
        private async static Task TryLoadSettings(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { SettingsViewModel: ILoadWriteSettings settingsViewModel })
                await settingsViewModel.LoadSettings();
        }
        
        private void TryRegisterMouseHooks(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { RecordingControlViewModel: IMouseHookControl recordingControlViewModel })
                recordingControlViewModel.RegisterHooks();
        }
        
        private void TryUnregisterMouseHooks(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { RecordingControlViewModel: IMouseHookControl recordingControlViewModel })
                recordingControlViewModel.DeregisterHooks();
        }

        #endregion
    }

}