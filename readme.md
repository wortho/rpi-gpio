# ASP.NET Web API for controlling Raspberry Pi GPIO with camjam motors.

## build

dotnet publish -c Release -r linux-arm

## deploy to Pi

scp -r bin/Release/netcoreapp2.0/linux-arm/publish  pi@192.168.0.23:projects/rpi.gpio

scp -r bin/Release/netcoreapp2.0/linux-arm/publish/rpi.gpio*.*  pi@192.168.0.23:projects/rpi.gpio

scp -r bin/Release/netcoreapp2.0/linux-arm/publish/rpi.gpio  pi@192.168.0.23:projects/rpi.gpio

## run on pi

cd home/pi/projects/rpi.gpio/
chmod +x rpi.gpio
export ASPNETCORE_URLS="http://*:5000"
./rpi.gpio

## call endpoints

curl http://192.168.0.23:5000

curl http://192.168.0.23:5000/api/status

curl http://192.168.0.23:5000/api/gpio

curl -X PUT -H "Content-Type: application/json" -d null http://192.168.0.23:5000/api/motors 

curl -X PUT -H "Content-Type: application/json" -d 'true' http://192.168.0.23:5000/api/motor/0