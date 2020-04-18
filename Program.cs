using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

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
           UltrasonicSensor();
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
           const int Trigger = 21;
           const int Echo = 20;
           GpioController controller = new GpioController ();
           controller.OpenPin (Trigger,PinMode.Output);
           controller.OpenPin (Echo,PinMode.Input);
           Console.WriteLine ("UltrasonicSensor.");
           Console.WriteLine ($"Echo default signal:{controller.Read(Echo)} " );
       } 
    }
}
