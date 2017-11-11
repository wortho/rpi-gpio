using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Model
{
    public static class Distance
    { 
        public const WiringPiPin TriggerPin = WiringPiPin.Pin00;
        public const WiringPiPin EchoPin = WiringPiPin.Pin01;
        public static decimal MeasureDistance(ILogger logger)
        {
            GpioPin triggerPin = Pi.Gpio[TriggerPin];
            GpioPin echoPin = Pi.Gpio[EchoPin];
            triggerPin.PinMode = GpioPinDriveMode.Output;
            echoPin.PinMode = GpioPinDriveMode.Input;

            triggerPin.Write(false);
            Pi.Timing.SleepMicroseconds(500000);
            triggerPin.Write(true);
            Pi.Timing.SleepMicroseconds(10);
            triggerPin.Write(false);

            var stopwatch = Stopwatch.StartNew();
            //Loop until the Echo pin is taken high (==1)
            while (!echoPin.Read())
            {
                stopwatch.Restart(); 
            }
            // Loop until it goes low again
            while (echoPin.Read() && stopwatch.ElapsedMilliseconds < 1000);
            stopwatch.Stop();
            var duration = (decimal) stopwatch.ElapsedTicks / (decimal) Stopwatch.Frequency;
            var distance = CalulateDistance(duration);
            logger.LogInformation($"Distance: {distance}");
            return distance;
        }

        public static string GetStatus(GpioPin pin) =>
            $" Name:{pin.Name} Mode:{pin.PinMode} Value:{pin.ReadValue()}";

        public static decimal CalulateDistance(decimal secs) => (secs * 343.26m) / 2.0m;
    }
}