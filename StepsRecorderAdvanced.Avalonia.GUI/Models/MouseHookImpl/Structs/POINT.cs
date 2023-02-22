using System.Runtime.InteropServices;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models.MouseHookImpl.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}