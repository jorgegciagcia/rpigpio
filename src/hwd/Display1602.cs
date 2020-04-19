using System;
using System.Device.Gpio;
using System.Threading;
namespace rpigpio.src.hwd
{
    public class Display1602 : HardwareModule
    {
        public int Reset{get;set;}
        public int Enable{get;set;}
        public int D4{get;set;}
        public int D5{get;set;}
        public int D6{get;set;}
        public int D7{get;set;}

        private GpioController _controller;

        public Display1602 (int reset, int enable, int d4, int d5, int d6, int d7)
        {
           Reset = reset;
           Enable = enable;
           D4 = d4;
           D5 = d5;
           D6 = d6;
           D7 = d7;   
           Configure();
           InitLevels();
           InitHardware ();
        }       
       public void Configure ()
       {
           _controller = new GpioController();
           _controller.OpenPin (Reset,PinMode.Output);
           _controller.OpenPin (Enable,PinMode.Output);
           _controller.OpenPin (D4,PinMode.Output);
           _controller.OpenPin (D5,PinMode.Output);
           _controller.OpenPin (D6,PinMode.Output);
           _controller.OpenPin (D7,PinMode.Output);
       }
       public void InitLevels ()
       {
           _controller.Write(Reset,PinValue.Low);
           _controller.Write(Enable,PinValue.Low);
           _controller.Write(D4,PinValue.Low);
           _controller.Write(D5,PinValue.Low);
           _controller.Write(D6,PinValue.Low);
           _controller.Write(D7,PinValue.Low);
       }
      public void InitHardware ()
      {
          ResetLine(false);
          Thread.Sleep(15);
          writeNibble (0x3);
          Thread.Sleep(5);
          writeNibble (0x3);
          Thread.Sleep(1);
          writeNibble(0x3);
          Thread.Sleep(5);
          writeNibble(0x2);
          Thread.Sleep(1);
          Command(0x28);
          Thread.Sleep(5);
          Command(0x0C);
          Thread.Sleep(5);
          Command(0x06);
          Thread.Sleep(1);
          Command(0x80);
          Thread.Sleep(5);
      } 
      private void writeNibble (byte value)
     {
         _controller.Write(Enable,PinValue.Low);
         _controller.Write(D4,value&0x1);
         _controller.Write(D5,value>>1&0x1);
         _controller.Write(D6,value>>2&0x1);
         _controller.Write(D7,value>>3&0x1);
         Strobe();
     }
     private void ResetLine(bool status)
     {
         _controller.Write(Reset, status ? PinValue.High : PinValue.Low);
     }
     private void Command (byte cmd)
    {
        ResetLine(false);
        byte cmdValue = (byte)((cmd >> 4)&0x0F);
        writeNibble(cmdValue);
        Thread.Sleep(1);
        writeNibble((byte)(cmd&0xF));
        Thread.Sleep(1);
    }
    private void Data (byte cmd)
    {
        ResetLine(true);
        byte cmdValue = (byte)((cmd >> 4)&0x0F);
        writeNibble(cmdValue);
        Thread.Sleep(1);
        writeNibble((byte)(cmd&0xF));
        Thread.Sleep(1);  
    } 
    public void Write (string str)
    {
        foreach(char a in str)
       { 
           Data((byte)a);
           Thread.Sleep(4);
       } 
    }
    public void Clear ()
    {
       Command(0x1); 
    }
    public void CursorOn ()
   {
       Command(0xE);
   }  
   public void CursorOff ()
   {
      Command(0xC);
   } 
   public void SetCursor (int x,int y)
  {
      byte command = 0x80;
      if (y == 2)
      {
         command = 0xC0;
      } 
      command += (byte)(x-1);
      Command (command);
  } 
     private void Strobe()
    {
        Thread.Sleep(1);
        _controller.Write(Enable,PinValue.High);
        Thread.Sleep(1);
        _controller.Write(Enable,PinValue.Low);
    } 
    } 
}