# ASP.NET Web API for controlling Raspberry Pi GPIO with camjam motors.
# Based on the example https://carlos.mendible.com/2017/09/01/toggle-raspberry-pi-gpio-pins-with-asp-net-core-20/

## build

dotnet publish -c Release -r linux-arm

## initial deploy to Pi

scp -r bin/Release/netcoreapp2.0/linux-arm/publish  pi@192.168.0.23:projects/rpi.gpio

## update after rebuild
scp -r bin/Release/netcoreapp2.0/linux-arm/publish/rpi.gpio*  pi@192.168.0.23:projects/rpi.gpio
scp -r bin/Release/netcoreapp2.0/linux-arm/publish/wwwroot/*  pi@192.168.0.23:projects/rpi.gpio/wwwroot

## run on pi
ssh pi@192.168.0.23

sudo -i
cd /home/pi/projects/rpi.gpio/
chmod +x rpi.gpio
export ASPNETCORE_URLS="http://*:5000"
./rpi.gpio

## call status endpoints

curl http://192.168.0.23:5000

curl http://192.168.0.23:5000/api/status

curl http://192.168.0.23:5000/api/gpio

## turn off all motors
curl -X PUT -H "Content-Type: application/json" -d 'false' http://192.168.0.23:5000/api/motor 

## left motor forwards
curl -X PUT -H "Content-Type: application/json" -d 'true' http://192.168.0.23:5000/api/motor/0

## right motor backwards
curl -X PUT -H "Content-Type: application/json" -d 'false' http://192.168.0.23:5000/api/motor/1

## both motors forwards
curl -X PUT -H "Content-Type: application/json" -d 'true' http://192.168.0.23:5000/api/motor/2

## both motors backwards
curl -X PUT -H "Content-Type: application/json" -d 'false' http://192.168.0.23:5000/api/motor/2