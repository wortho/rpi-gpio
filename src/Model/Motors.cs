using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Model
{
    public static class Motors
    {    
        public const WiringPiPin LeftMotorForwardsPin = WiringPiPin.Pin10;
        public const WiringPiPin LeftMotorReversePin = WiringPiPin.Pin11;
        public const WiringPiPin RightMotorForwardsPin = WiringPiPin.Pin12;
        public const WiringPiPin RightMotorReversePin = WiringPiPin.Pin13;

        public static string GetStatus(WiringPiPin wiringPiPin)
        {
            var pin = Pi.Gpio[wiringPiPin];
            return $"WiringPiPinNumber: {pin.WiringPiPinNumber} BcmPinNumber: {pin.BcmPinNumber} Name:{pin.Name} Mode:{pin.PinMode} SoftPwmValue:{pin.SoftPwmValue}";
        }

        public static string[] GetMotorPinStatus() => new[] {
                GetStatus(LeftMotorForwardsPin),
                GetStatus(LeftMotorReversePin),
                GetStatus(RightMotorForwardsPin),
                GetStatus(RightMotorReversePin)};


        public static void SetAllPinModes(GpioPinDriveMode mode)
        {
            Pi.Gpio[LeftMotorReversePin].PinMode = mode;
            Pi.Gpio[LeftMotorForwardsPin].PinMode = mode;
            Pi.Gpio[RightMotorReversePin].PinMode = mode;
            Pi.Gpio[RightMotorForwardsPin].PinMode = mode;
        }

        public static void LeftMotor(bool forwards)
        {
            Motors.SetAllPinModes(GpioPinDriveMode.Output);
            Pi.Gpio[LeftMotorForwardsPin].Write(forwards);
            Pi.Gpio[LeftMotorReversePin].Write(!forwards);
        }

        public static void TurnMotorsOff()
        {
            //Turn all motors off
            Motors.SetAllPinModes(GpioPinDriveMode.Output);
            Pi.Gpio[LeftMotorReversePin].Write(false);
            Pi.Gpio[LeftMotorForwardsPin].Write(false);
            Pi.Gpio[RightMotorReversePin].Write(false);
            Pi.Gpio[RightMotorForwardsPin].Write(false);
        }

        public static void RightMotor(bool forwards)
        {
            Motors.SetAllPinModes(GpioPinDriveMode.Output);
            Pi.Gpio[RightMotorReversePin].Write(!forwards);
            Pi.Gpio[RightMotorForwardsPin].Write(forwards);
        }
    }
}