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
        public const WiringPiPin LeftMotorAPin = WiringPiPin.Pin11;
        public const WiringPiPin LeftMotorBPin = WiringPiPin.Pin10;
        public const WiringPiPin RightMotorAPin = WiringPiPin.Pin13;
        public const WiringPiPin RightMotorBPin = WiringPiPin.Pin12;

        public static string GetStatus(WiringPiPin wiringPiPin)
        {
            var pin = Pi.Gpio[wiringPiPin];
            return $"WiringPiPinNumber: {pin.WiringPiPinNumber} BcmPinNumber: {pin.BcmPinNumber} Name:{pin.Name} Mode:{pin.PinMode}";
        }

        public static string GetPinStatus(string name, WiringPiPin wiringPiPin)
        {
            var pin = Pi.Gpio[wiringPiPin];
            pin.PinMode = GpioPinDriveMode.Input;
            var pinStatus = pin.Read();
            pin.PinMode = GpioPinDriveMode.Output;
            return $"{name} Pin:{pin.WiringPiPinNumber} Value:{pinStatus}";
        }

        public static string[] GetMotorPinStatus() => new[] {
                GetPinStatus("LeftMotorAPin",LeftMotorAPin),
                GetPinStatus("LeftMotorBPin",LeftMotorBPin),
                GetPinStatus("RightMotorAPin",RightMotorAPin),
                GetPinStatus("RightMotorBPin",RightMotorBPin)};


        private static void SetAllPinModes(GpioPinDriveMode mode)
        {
            Pi.Gpio[LeftMotorAPin].PinMode = mode;
            Pi.Gpio[LeftMotorBPin].PinMode = mode;
            Pi.Gpio[RightMotorAPin].PinMode = mode;
            Pi.Gpio[RightMotorBPin].PinMode = mode;
        }

        public static void LeftMotor(bool forwards)
        {
            Pi.Gpio[LeftMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[LeftMotorBPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[LeftMotorAPin].Write(!forwards);
            Pi.Gpio[LeftMotorBPin].Write(forwards);
        }

        public static void TurnMotorsOff()
        {
            //Turn all motors off
            Motors.SetAllPinModes(GpioPinDriveMode.Output);
            Pi.Gpio[LeftMotorAPin].Write(false);
            Pi.Gpio[LeftMotorBPin].Write(false);
            Pi.Gpio[RightMotorAPin].Write(false);
            Pi.Gpio[RightMotorBPin].Write(false);
        }

        public static void RightMotor(bool forwards)
        {
            Pi.Gpio[RightMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorBPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorAPin].Write(!forwards);
            Pi.Gpio[RightMotorBPin].Write(forwards);
        }
    }
}