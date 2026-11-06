using System;

namespace HallMonitor.Core
{
    public class Reading
    {
        public Sensor Sensor { get; }
        public double Value { get; }
        public DateTime Time { get; }

        public Reading(Sensor sensor, double value, DateTime time)
        {
            Sensor = sensor;
            Value = value;
            Time = time;
        }
    }
}