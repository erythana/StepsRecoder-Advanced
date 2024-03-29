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
            if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                throw new NotSupportedException("Platform not supported");

            desktop.MainWindow = new MainWindow();
            var dataContext = desktop.MainWindow.DataContext;
            desktop.Startup += async (_, _) =>
                await StartupProcedures(dataContext);
            desktop.Exit += async (_, _) =>
                await ExitProcedures(dataContext);


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
        
        private static async Task StartupProcedures(object? dataContext)
        {
            await TryLoadSettings(dataContext);
        }

        private static async Task ExitProcedures(object? dataContext)
        {
            await TrySaveSettings(dataContext);
        }

        #endregion
        
        #region helper methods
        
        private static async Task TrySaveSettings(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { SettingsViewModel: ILoadWriteSettings settingsViewModel })
                await settingsViewModel.SaveSettings();
        }
        
        private static async Task TryLoadSettings(object? dataContext)
        {
            if (dataContext is MainWindowViewModel { SettingsViewModel: ILoadWriteSettings settingsViewModel })
                await settingsViewModel.LoadSettings();
        }

        #endregion
    }

}