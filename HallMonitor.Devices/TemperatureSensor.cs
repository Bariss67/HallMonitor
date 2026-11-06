using System;
using HallMonitor.Core;

namespace HallMonitor.Devices
{
    public class TemperatureSensor : Sensor
    {
        public double LastValue { get; private set; }
        public DateTime LastTime { get; private set; }

        public TemperatureSensor(string name, double limit) : base(name, limit)
        {
            LastValue = 20.0;
            LastTime = DateTime.Now;
        }

        protected override double Measure()
        {
            LastValue = 20 + Random.Shared.NextDouble() * 7;
            LastTime = DateTime.Now;
            return LastValue;
        }

        public override string GetStatus()
        {
            if (!IsRunning)
                return "stopped";

            return $"measuring, {LastValue.ToString("0.0")} C at {LastTime.ToString("HH:mm:ss")}";
        }
    }
}