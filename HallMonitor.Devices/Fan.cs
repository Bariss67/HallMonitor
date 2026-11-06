using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class Fan : Device
    {
        private readonly int _rpm;

        public Fan(string name, int rpm) : base(name)
        {
            _rpm = rpm;
        }

        public override string GetStatus()
        {
            if (!IsRunning)
                return "stopped";

            return $"running, {_rpm} rpm";
        }
    }
}