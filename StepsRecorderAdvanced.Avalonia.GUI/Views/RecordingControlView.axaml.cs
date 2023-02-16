using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepsRecorderAdvanced.Avalonia.GUI.Views
{
    public partial class RecordingControlView : UserControl
    {
        public RecordingControlView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}