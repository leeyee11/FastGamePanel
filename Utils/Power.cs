using System.Management;
using System.Runtime.InteropServices;

namespace FastGamePanel.Utils
{
    internal class Power
    {
        public static Guid HighPerformanceGuid = new Guid("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

        public static Guid BalancedGuid = new Guid("381b4222-f694-41f0-9685-ff5bb260df2e");

        public static Guid PowerSaverGuid = new Guid("a1841308-3541-4fab-bc81-f71556f20b4a");

        private const int ERROR_SUCCESS = 0;

        [DllImport("powrprof.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern uint PowerSetActiveScheme(IntPtr UserPowerKey, ref Guid SchemeGuid);

        [DllImport("powrprof.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern uint PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);

        public static void SetPowerPlan(Guid schemeGuid)
        {
            uint result = PowerSetActiveScheme(IntPtr.Zero, ref schemeGuid);
            if (result != ERROR_SUCCESS)
            {
                throw new Exception("Failed to set power plan.");
            }
        }

        public static Guid GetActivePowerPlan()
        {
            uint result = PowerGetActiveScheme(IntPtr.Zero, out IntPtr activePolicyGuid);
            if (result != ERROR_SUCCESS)
            {
                throw new Exception("Failed to get active power plan.");
            }

            Guid activeGuid = (Guid)Marshal.PtrToStructure(activePolicyGuid, typeof(Guid));
            //Marshal.FreeHGlobal(activePolicyGuid);

            return activeGuid;
        }
    }
}
