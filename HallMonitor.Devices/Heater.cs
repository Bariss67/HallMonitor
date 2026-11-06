using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class Heater : Device
    {
        private readonly double _powerKw;

        public Heater(string name, double powerKw) : base(name)
        {
            _powerKw = powerKw;
        }

        public override string GetStatus()
        {
            if (!IsRunning)
                return "stopped";

            return $"heating, {_powerKw.ToString("0.0")} kW";
        }
    }
}