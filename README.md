# rpigpio
Raspberrypi GPIO

## GPIO PINOUT

Inline-style: 
![alt text](https://docs.microsoft.com/en-us/windows/iot-core/media/pinmappingsrpi/rp2_pinout.png "Raspberry PI pinout")

## CONFIGURE ENVIRONMENT FOR DOTNET CORE 3.1

### ADD RASPBERRY PI PACKAGES TO PROJECT

dotnet add package System.Device.Gpio --source https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json

dotnet add package Iot.Device.Bindings --source https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json

### SET PERMISSIONS

 sudo chmod 777 /dev/gpiomem
 
