using System;
using Avalonia;
using Avalonia.Xaml.Interactivity;
using AvaloniaEdit;

namespace StepsRecorder.Avalonia.GUI.Controls.AvalonEdit.Behaviors;

public class DocumentTextBindingBehavior : Behavior<TextEditor>
{
    #region member fields

    private TextEditor textEditor;

    #endregion

    #region DependencyProperties (Styled Properties)

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<DocumentTextBindingBehavior, string>(nameof(Text));

    #endregion

    #region Properties

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region Overrides

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is not { } avaloniaEditor) return;

        textEditor = avaloniaEditor;
        textEditor.TextChanged += TextChanged;
        this.GetObservable(TextProperty).Subscribe(TextPropertyChanged);
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        textEditor.TextChanged -= TextChanged;
    }

    #endregion

    #region EventHandler

    private void TextChanged(object sender, EventArgs eventArgs)
    {
        if (textEditor.Document != null)
            Text = textEditor.Document.Text;
    }

    private void TextPropertyChanged(string? text)
    {
        if (textEditor.Document == null || text == null) return;

        var caretOffset = textEditor.CaretOffset;
        textEditor.Document.Text = text;
        textEditor.CaretOffset = Math.Clamp(caretOffset, 0, text.Length);
    }

    #endregion
}