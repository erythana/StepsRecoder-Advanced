using System.Runtime.InteropServices;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models.NativeMonitorImpl.Structs
{
    /// <summary>
    /// Rectangle
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}