using System.Runtime.InteropServices;
using Windows.Gaming.Input;

namespace FastGamePanel.Utils;

internal class GamepadRemapper
{
    public static void Start ()
    {
        var controllers = RawGameController.RawGameControllers;

        // 订阅按键事件
        foreach (var controller in controllers)
        {
            var buttonStates = new bool[controller.ButtonCount];
            var axisPositions = new double[controller.AxisCount];
            var switchpositions = new GameControllerSwitchPosition[controller.SwitchCount];
            controller.GetCurrentReading(buttonStates, switchpositions, axisPositions);
            Console.WriteLine(buttonStates);
        }
    }
}