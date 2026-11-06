using HallMonitor.Core;
using HallMonitor.Devices;
using HallMonitor.UI;

class Program
{
    static void Main(string[] args)
    {
        Hall hall = new Hall();

        hall.AddDevice(new TemperatureSensor("T-hall", 26.0));
        hall.AddDevice(new Fan("Fan-1", 1430));
        hall.AddDevice(new Heater("Heater-1", 2.5));
        hall.AddDevice(new Conveyor("Conveyor-1", 1.5));

        hall.StartAll();

        Menu menu = new Menu(hall);
        menu.Run();

        hall.StopAll();
    }
}