using System;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Enums;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;

namespace StepsRecorderAdvanced.Avalonia.GUI.Models
{
    public class XPlatformScreenshotUtilityFactory : IXPlatformScreenshotUtilityFactory
    {
        #region member fields

        private IDisplayProtocolIdentifier displayIdentifier;
        private DisplayProtocolEnum? displayServer;

        #endregion

        #region constructor

        public XPlatformScreenshotUtilityFactory(IDisplayProtocolIdentifier displayIdentifier)
        {
            this.displayIdentifier = displayIdentifier;
        }

        #endregion

        #region Factory methods

        public IScreenshotUtility Create()
        {
            displayServer ??= displayIdentifier.GetDisplayProtocol();
            return displayServer switch
            {
                DisplayProtocolEnum.Windows => new WindowsScreenshotUtility(),
                DisplayProtocolEnum.X11 => new X11ScreenshotUtility(),
                DisplayProtocolEnum.Wayland => new WaylandScreenshotUtility(),
                _ => throw new NotSupportedException("The display server could not be recognized")
            };
        }

        #endregion
    }

}