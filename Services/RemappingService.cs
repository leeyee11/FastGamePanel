using FastGamePanel.Utils;

namespace FastGamePanel.Services;

public class RemappingService
{
    public bool GetIsRemappingEnabled()
    {
        return KeyboardRemapper.GetRemappingEnabled();
    }

    public void SetKeyboardHook()
    {
        KeyboardRemapper.SetKeyboardHook();
    }

    public void UnsetKeyboardHook()
    {
        KeyboardRemapper.UnsetKeyboardHook();
    }

    public void EnableRemapping()
    {
        //GamepadRemapper.Start();
        KeyboardRemapper.EnableRemapping();
    }

    public void DisableRemapping()
    {
        KeyboardRemapper.DisableRemapping();
    }

    public Task<int> GetNextKeystroke()
    {
        return KeyboardRemapper.GetNextKeystroke();
    }
}
