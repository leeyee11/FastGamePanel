using FastGamePanel.Utils;
using FastGamePanel.Common;

namespace FastGamePanel.Services
{
    internal class PowerSettingService
    {
        public PowerMode GetPowerMode()
        {
            Guid currentPlan = Power.GetActivePowerPlan();
            if (currentPlan == Power.PowerSaverGuid)
            {
                return PowerMode.PowerSaver;
            }
            else if (currentPlan == Power.BalancedGuid)
            { 
                return PowerMode.Balanced;
            }
            else if (currentPlan == Power.HighPerformanceGuid)
            {
                return PowerMode.HighPerformance;
            }
            else
            {
                return PowerMode.Custom;
            }
        }
        public void SwitchPowerMode(PowerMode mode)
        {
            if (mode == PowerMode.PowerSaver)
            {
                Power.SetPowerPlan(Power.PowerSaverGuid);
            } 
            else if (mode == PowerMode.Balanced)
            {
                Power.SetPowerPlan(Power.BalancedGuid);
            }
            else if (mode == PowerMode.HighPerformance)
            {
                Power.SetPowerPlan(Power.HighPerformanceGuid);
            }
            else
            {
                // TODO: Support TDP customize
            }
        }
    }
}
