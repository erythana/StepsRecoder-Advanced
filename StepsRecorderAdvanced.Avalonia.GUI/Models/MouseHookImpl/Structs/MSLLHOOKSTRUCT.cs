using System;
using System.Runtime.InteropServices;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models.MouseHookImpl.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}