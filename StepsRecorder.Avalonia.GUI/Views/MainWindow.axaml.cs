using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using StepsRecorder.Avalonia.GUI.DependencyInjection;
using StepsRecorder.Avalonia.GUI.ViewModels;
using StepsRecorder.Avalonia.GUI.ViewModels.Interfaces;

namespace StepsRecorder.Avalonia.GUI.Views
{
    public partial class MainWindow : Window
    {
        #region member fields

        private bool mouseDownForWindowMoving = false;
        private PointerPoint originalPoint;

        #endregion
        
        public MainWindow()
        {
            InitializeComponent();
            var container = ContainerConfig.Configure();
            
            using var scope = container.BeginLifetimeScope();
            DataContext = scope.Resolve<IMainWindowViewModel>();
        }

        #region Event Handler

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.Key == Key.V && (e.KeyModifiers & KeyModifiers.Control) == KeyModifiers.Control &&
            //     sender is Control { DataContext: MainWindowViewModel { SettingViewModel: { } tvm } })
            //     tvm.TryFilterTargets();//TODO: Change to something like TryFilterTargets and filter the list and check it accordingly
        }



        private void MainWindow_OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (!mouseDownForWindowMoving) return;

            var currentPoint = e.GetCurrentPoint(this);
            Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - originalPoint.Position.X),
                Position.Y + (int)(currentPoint.Position.Y - originalPoint.Position.Y));
        }

        private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // if (WindowState is WindowState.Maximized or WindowState.FullScreen) return; //TODO: wrong behaviour on drop down!
            //
            // mouseDownForWindowMoving = true;
            // originalPoint = e.GetCurrentPoint(this);
        }

        private void MainWindow_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            mouseDownForWindowMoving = false;
        }

        #endregion Event Handler
    }
}