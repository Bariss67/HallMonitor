using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class Conveyor : Device
    {
        private readonly double _speedMps; 

        public Conveyor(string name, double speedMps) : base(name)
        {
            _speedMps = speedMps;
        }

        public override string GetStatus()
        {
            if (!IsRunning)
                return "stopped";

            return $"running, {_speedMps.ToString("0.0")} m/s";
        }
    }
}