using System;

namespace HallMonitor.Core
{
    public abstract class Sensor : Device
    {
        public double Limit { get; }
        private readonly System.Timers.Timer _timer;

        public event EventHandler<Reading>? LimitExceeded;

        protected Sensor(string name, double limit) : base(name)
        {
            Limit = limit;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTick;
        }

        public override void Start()
        {
            base.Start();
            _timer.Start();
        }

        public override void Stop()
        {
            base.Stop();
            _timer.Stop();
        }

        private void OnTick(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsRunning) return;

            double value = Measure();
            var reading = new Reading(this, value, DateTime.Now);

            if (value > Limit)
            {
                LimitExceeded?.Invoke(this, reading);
            }
        }

        protected abstract double Measure();
    }
}