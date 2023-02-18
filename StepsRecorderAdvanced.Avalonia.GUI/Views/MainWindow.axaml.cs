using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels;
using StepsRecorderAdvanced.Avalonia.GUI.DependencyInjection;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var container = ContainerConfig.Configure();
            
            using var scope = container.BeginLifetimeScope();
            DataContext = scope.Resolve<IMainWindowViewModel>();
        }
    }
}