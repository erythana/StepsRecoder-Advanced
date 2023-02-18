using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels;
using StepsRecorderAdvanced.Avalonia.GUI.Views;

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
                {
                    if (dataContext is MainWindowViewModel { SettingsViewModel: {} settingsViewModel})
                        await settingsViewModel.LoadSettings();
                };
                desktop.Exit +=  async (_, _) =>
                {
                    if (dataContext is MainWindowViewModel { SettingsViewModel: {} settingsViewModel})
                        await settingsViewModel.SaveSettings();
                };
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
    }
}