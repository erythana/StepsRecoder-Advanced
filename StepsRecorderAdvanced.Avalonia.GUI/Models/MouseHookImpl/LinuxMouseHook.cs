namespace StepsRecorderAdvanced.Avalonia.GUI.Models.MouseHookImpl
{
    public class LinuxMouseHook : IMouseHook
    {

        public event WindowsMouseHook.MouseHookCallback? LeftButtonDown;
        public event WindowsMouseHook.MouseHookCallback? LeftButtonUp;
        public event WindowsMouseHook.MouseHookCallback? RightButtonDown;
        public event WindowsMouseHook.MouseHookCallback? RightButtonUp;
        public event WindowsMouseHook.MouseHookCallback? MouseMove;
        public event WindowsMouseHook.MouseHookCallback? MouseWheel;
        public event WindowsMouseHook.MouseHookCallback? DoubleClick;
        public event WindowsMouseHook.MouseHookCallback? MiddleButtonDown;
        public event WindowsMouseHook.MouseHookCallback? MiddleButtonUp;
        public void Install()
        {
        }

        public void Uninstall()
        {
        }
    }
}