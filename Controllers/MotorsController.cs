using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Controllers
{
    [Route("api/[controller]")]
    public class MotorController : Controller
    {
        private const WiringPiPin LeftMotorAPin = WiringPiPin.Pin11;
        private const WiringPiPin LeftMotorBPin = WiringPiPin.Pin10;
        private const WiringPiPin RightMotorAPin = WiringPiPin.Pin13;
        private const WiringPiPin RightMotorBPin = WiringPiPin.Pin12;
        private readonly ILogger logger;

        public MotorController(ILogger<MotorController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPut]
        public void PutMotors([FromBody] bool on)
        {
            SetPinModes();
            TurnMotorsOff();
            LogPinStatus(Pi.Gpio[LeftMotorAPin]);
            LogPinStatus(Pi.Gpio[LeftMotorBPin]);
            LogPinStatus(Pi.Gpio[RightMotorAPin]);
            LogPinStatus(Pi.Gpio[RightMotorBPin]);
        }

        private void LogPinStatus(GpioPin pin)
        {
            logger.LogInformation($"WiringPiPinNumber: {pin.WiringPiPinNumber} BcmPinNumber: {pin.BcmPinNumber} Name:{pin.Name} Mode:{pin.PinMode}");
        }

        [HttpPut("{id}")]
        public void PutMotor(int id, [FromBody] bool forward)
        {
            switch (id)
            {
                case 0:
                    LeftMotor(forward);
                    break;
                case 1:
                    RightMotor(forward);
                    break;
                default:
                    break;
            }
        }

        private static void SetPinModes()
        {
            Pi.Gpio[LeftMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[LeftMotorBPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorBPin].PinMode = GpioPinDriveMode.Output;
        }

        private static void LeftMotor(bool forwards)
        {
            Pi.Gpio[LeftMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[LeftMotorBPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[LeftMotorAPin].Write(!forwards);
            Pi.Gpio[LeftMotorBPin].Write(forwards);
        }

        private static void TurnMotorsOff()
        {
            //Turn all motors off
            Pi.Gpio[LeftMotorAPin].Write(false);
            Pi.Gpio[LeftMotorBPin].Write(false);
            Pi.Gpio[RightMotorAPin].Write(false);
            Pi.Gpio[RightMotorBPin].Write(false);
        }

        private static void RightMotor(bool forwards)
        {
            Pi.Gpio[RightMotorAPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorBPin].PinMode = GpioPinDriveMode.Output;
            Pi.Gpio[RightMotorAPin].Write(!forwards);
            Pi.Gpio[RightMotorBPin].Write(forwards);
        }
    }
}
