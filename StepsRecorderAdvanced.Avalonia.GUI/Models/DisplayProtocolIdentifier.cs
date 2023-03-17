using System;
using System.Runtime.InteropServices;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Enums;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models
{
    public class DisplayProtocolIdentifier : IDisplayProtocolIdentifier
    {
        public DisplayProtocolEnum GetDisplayProtocol()
        {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return DisplayProtocolEnum.Windows;
            
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    //TODO: https://stackoverflow.com/questions/45536141/how-i-can-find-out-if-a-linux-system-uses-wayland-or-x11/45537237#45537237
                    //Maybe change to the stuff from the link - check when switching session to x11 
                    var protocol = Environment.GetEnvironmentVariable("XDG_SESSION_TYPE")?.ToLower();
                    if (protocol == "x11")
                        return DisplayProtocolEnum.X11;
                    else if (protocol == "wayland")
                        return DisplayProtocolEnum.Wayland;
                }

                return DisplayProtocolEnum.Unsupported;
        }
    }
}