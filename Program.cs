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
        public static int GPIO17=17;
        static void Main(string[] args)
        {
            GpioController controller = new GpioController ();
            controller.OpenPin (GPIO17,PinMode.Output);
            int timeUp = 5000;
            int timeLow = 5000;

            Console.WriteLine("Hello World!");
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
    }
}
