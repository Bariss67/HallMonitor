using System;
using System.Collections.Generic;

namespace HallMonitor.Core
{
    public class Hall
    {
        private readonly List<Device> _devices = new List<Device>();
        private readonly List<Reading> _alarmHistory = new List<Reading>();

        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();
        public IReadOnlyList<Reading> AlarmHistory => _alarmHistory.AsReadOnly();

        public event EventHandler<Reading>? AlarmRaised;

        public void AddDevice(Device device)
        {
            _devices.Add(device);
            if (device is Sensor sensor)
            {
                sensor.LimitExceeded += OnSensorLimitExceeded;
            }
        }

        private void OnSensorLimitExceeded(object? sender, Reading reading)
        {
            lock (_alarmHistory)
            {
                _alarmHistory.Add(reading);
            }
            AlarmRaised?.Invoke(this, reading);
        }

        public void StartAll()
        {
            foreach (var device in _devices)
            {
                device.Start();
            }
        }

        public void StopAll()
        {
            foreach (var device in _devices)
            {
                device.Stop();
            }
        }
    }
}