using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaEdit;
using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Avalonia.GUI.Controls.AvalonEdit;
using StepsRecorderAdvanced.Avalonia.GUI.ViewModels.Interfaces;
using StepsRecorderAdvanced.Core.Models.Enums;

namespace StepsRecorderAdvanced.Avalonia.GUI.Views
{
    public partial class LoggingView : UserControl
    {
        #region member fields

        private Func<LogTypeEnum, ISolidColorBrush> colorizerFunc = null;

        private EnumLineColorizer<LogTypeEnum> currentEnumLineColorizer = null;

        #endregion
        public LoggingView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            
            
            if (DataContext is not ILoggingViewModel lvm || lvm.LogTypeEnumColor.Equals(colorizerFunc)) return;
            
            SwapLineColorizer(lvm);
        }

        #region helper methdos

        private void SwapLineColorizer(ILoggingViewModel lvm)
        {
            var editor = this.FindControl<TextEditor>("AvaloniaEdit");
            var lineTransformers = editor.TextArea.TextView.LineTransformers;

            colorizerFunc = lvm.LogTypeEnumColor;

            lineTransformers.Remove(currentEnumLineColorizer);
            currentEnumLineColorizer = new EnumLineColorizer<LogTypeEnum>(lvm.LogTypeEnumColor);
            lineTransformers.Add(currentEnumLineColorizer);
        }

        #endregion
    }
}