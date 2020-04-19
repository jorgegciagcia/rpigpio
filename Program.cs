using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using rpigpio.src.hwd;

namespace rpi001
{
    class Program
    {
        /*
           Add GPIO Libraries:

           dotnet add package System.Device.Gpio --source https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json
           dotnet add package Iot.Device.Bindings --source https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json

           Exec this line for use GPIO:
           
           sudo chmod 777 /dev/gpiomem
        */

       static void Main(string[] args)
       {
           //UltrasonicSensor();
           //Display1602();
           UltrasonicSensorDisplay1602();
       }
       static void SingleLed ()
       {
           const int GPIO17 = 17;
            GpioController controller = new GpioController ();
            controller.OpenPin (GPIO17,PinMode.Output);
            int timeUp = 5000;
            int timeLow = 5000;

            Console.WriteLine("Single Led");
            while(true)
           {
               Console.WriteLine("Line Up");
               controller.Write(GPIO17,PinValue.High);
               Thread.Sleep(timeUp);
               Console.WriteLine("Line Down");
               controller.Write(GPIO17,PinValue.Low);
               Thread.Sleep(timeLow);
           } 
       }
       static void UltrasonicSensor ()
       {
           UltraSonicSensor sensor = new UltraSonicSensor(21,20,2000);
           while (true)
          {
              if ( sensor.ReadDistance() == false)
             {
                 Console.WriteLine("Error");
             } 
             else
             {
                  Console.WriteLine(String.Format("Milliseconds: {0:0.0000}",sensor.Milliseconds));
                  Console.WriteLine(String.Format("Distance: {0:0.0} cm",sensor.Distance));
                  Console.ReadLine();
             }
          } 
       } 
       static void Display1602 ()
      {
          Display1602 display = new Display1602(16,12,26,25,24,23);
          display.Clear();
          display.SetCursor(1,1);
          display.Write("Line 1");
          display.SetCursor(3,2);
          display.Write("Line 2");
      }
      static void UltrasonicSensorDisplay1602()
      {
           UltraSonicSensor sensor = new UltraSonicSensor(21,20,2000);
           Display1602 display = new Display1602(16,12,26,25,24,23);
            display.Clear();
           while (true)
           {
              if ( sensor.ReadDistance() == false)
             {
                 Console.WriteLine("Error");
             } 
             else
             {
                  Console.WriteLine(String.Format("Milliseconds: {0:0.0000}",sensor.Milliseconds));
                  Console.WriteLine(String.Format("Distance: {0:0.0} cm",sensor.Distance));
                  display.SetCursor(1,1);
                  display.Write(String.Format("Ms: {0:0.0000}",sensor.Milliseconds));
                  display.SetCursor(1,2);
                  display.Write(String.Format("Dt: {0:0.0} cm",sensor.Distance));
                  Console.ReadLine();
             }
           } 
      } 
    }
}
