using FastGamePanel.Utils;
using FastGamePanel.Common;

namespace FastGamePanel.Services;
public class DisplaySettingService
{
    public List<DisplayMode> GetSupportedDisplayModes()
    {
        return Display.GetSupportedDisplayModes();
    }

    public DisplayMode GetCurrentDisplayMode()
    {
        var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        DisplayMode mode;
        mode.width = (uint)mainDisplayInfo.Width;
        mode.height = (uint)mainDisplayInfo.Height;
        mode.frequency = (uint)mainDisplayInfo.RefreshRate;
        return mode;
    }

    public void ChangeDisplaySettings(int width, int height, int frequency)
    {
        Display.ChangeDisplaySettings(width, height, frequency);
    }
}

