dotnet publish -c Release -r linux-arm

scp -r bin/Release/netcoreapp2.0/linux-arm/publish  pi@192.168.0.23:projects/rpi.gpio

scp -r bin/Release/netcoreapp2.0/linux-arm/publish/rpi.gpio*.*  pi@192.168.0.23:projects/rpi.gpio

scp -r bin/Release/netcoreapp2.0/linux-arm/publish/rpi.gpio  pi@192.168.0.23:projects/rpi.gpio

curl 192.168.0.23:5000

curl -X POST -H 192.168.0.23:5000/api/motors "Content-Type: application/json" -d 'true'