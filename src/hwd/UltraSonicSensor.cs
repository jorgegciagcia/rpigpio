using System;
using System.Device.Gpio;
using System.Threading;

namespace rpigpio.src.hwd
{
    public class UltraSonicSensor
    {
        public int Trigger{get;set;} 
        public int Echo{get;set;}
        public int Timeout{get;set;} 
        private double _milliseconds;
        private double _distance;
        public double Milliseconds{get{return _milliseconds;}}
        public double Distance{get{return _distance;}}
        private GpioController _controller;
        public UltraSonicSensor (int trigger, int echo, int timeout)
       {
           Trigger = trigger;
           Echo = echo;
           Timeout = timeout;
           _controller = new GpioController();
           _controller.OpenPin (Trigger,PinMode.Output);
           _controller.OpenPin (Echo,PinMode.Input);
           ReadDistance();
       }
       public bool ReadDistance ()
       {
           _controller.Write (Trigger,PinValue.Low);
           Thread.Sleep(5);
           _controller.Write (Trigger,PinValue.High);
           Thread.Sleep(10);
           _controller.Write (Trigger,PinValue.Low);
           DateTime timeout = DateTime.Now.AddMilliseconds(Timeout);
           while (_controller.Read(Echo)==PinValue.Low)
              if (DateTime.Now > timeout)
                 return false;
           long startTicks = DateTime.Now.Ticks;
           timeout = DateTime.Now.AddMilliseconds(Timeout);
           while (_controller.Read(Echo)==PinValue.High)
              if (DateTime.Now > timeout)
                 return false;
           long endTicks = DateTime.Now.Ticks;
           _distance = (endTicks-startTicks)*34.32/(2*TimeSpan.TicksPerMillisecond);
           _milliseconds = (double)(endTicks-startTicks)/TimeSpan.TicksPerMillisecond;
           return true;
       } 
    }
}