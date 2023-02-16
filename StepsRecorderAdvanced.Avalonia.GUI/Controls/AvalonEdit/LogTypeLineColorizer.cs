using System;
using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using StepsRecorderAdvanced.Core.Models.Enums;

namespace StepsRecorderAdvanced.Avalonia.GUI.Controls.AvalonEdit;

public class EnumLineColorizer<T> : DocumentColorizingTransformer where T : struct, Enum
{
    #region member fields

    private Func<T, ISolidColorBrush> enumColorFunc;
    private T lastColoredEnum;

    #endregion

    #region constructor

    public EnumLineColorizer(Func<T, ISolidColorBrush> enumColorFunc)
    {
        this.enumColorFunc = enumColorFunc;
    }

    #endregion

    #region overrides
    
    protected override void ColorizeLine(DocumentLine line)
    {
        var text = CurrentContext.Document.GetText(line);
        foreach (var tEnum in Enum.GetValues<T>())
        {
            if (line.IsDeleted || !text.StartsWith(tEnum.ToString())) continue;
            
            lastColoredEnum = tEnum;
            ChangeLinePart(line.Offset, line.EndOffset, ApplyChanges);
        }
        
        void ApplyChanges(VisualLineElement element)
        {
            element.TextRunProperties.ForegroundBrush = EnumToBrush(lastColoredEnum, enumColorFunc);
        }
    }

    #endregion

    #region helper methods

    private static ISolidColorBrush EnumToBrush(T logType, Func<T, ISolidColorBrush> coloringFunc) =>
        coloringFunc(logType);

    #endregion

}