namespace rpigpio.src.hwd
{
    public interface HardwareModule
    {
         void Configure ();
         void InitLevels();
         void InitHardware ();
    }
}