using System;
using HallMonitor.Core;

namespace HallMonitor.UI
{
    public class Menu
    {
        private readonly Hall _hall;

        public Menu(Hall hall)
        {
            _hall = hall;
            _hall.AlarmRaised += OnAlarmRaised;
        }

        private void OnAlarmRaised(object? sender, Reading reading)
        {
            
            Console.WriteLine($"\n> !! ALARM {reading.Time.ToString("HH:mm:ss")} {reading.Sensor.Name} {reading.Value.ToString("0.0")} C, limit {reading.Sensor.Limit} C");
            Console.Write("> ");
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMainOptions();
                Console.Write("> ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowDevices();
                        break;
                    case "2":
                        ToggleDeviceMenu();
                        break;
                    case "3":
                        ShowAlarmHistory();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("unknown option, again");
                        break;
                }
            }
        }

        private void PrintMainOptions()
        {
            Console.WriteLine("=== Hall monitor ===");
            Console.WriteLine("1 Devices");
            Console.WriteLine("2 Start or stop a device");
            Console.WriteLine("3 Alarm history");
            Console.WriteLine("0 Exit");
        }

        private void ShowDevices()
        {
            var devices = _hall.Devices;
            for (int i = 0; i < devices.Count; i++)
            {
                var d = devices[i];
                Console.WriteLine($"{i + 1} {d.Name}: {d.GetStatus()}");
            }
        }

        private void ToggleDeviceMenu()
        {
            ShowDevices();
            Console.Write("device: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index >= 1 && index <= _hall.Devices.Count)
            {
                var device = _hall.Devices[index - 1];
                if (device.IsRunning)
                {
                    device.Stop();
                    Console.WriteLine($"{device.Name}: stopped");
                }
                else
                {
                    device.Start();
                    Console.WriteLine($"{device.Name}: started");
                }
            }
            else
            {
                Console.WriteLine("no such device, again");
            }
        }

        private void ShowAlarmHistory()
        {
            var history = _hall.AlarmHistory;
            if (history.Count == 0)
            {
                Console.WriteLine("No alarms recorded.");
                return;
            }

            foreach (var alarm in history)
            {
                Console.WriteLine($"{alarm.Time.ToString("HH:mm:ss")} {alarm.Sensor.Name} {alarm.Value.ToString("0.0")} C, limit {alarm.Sensor.Limit} C");
            }
        }
    }
}