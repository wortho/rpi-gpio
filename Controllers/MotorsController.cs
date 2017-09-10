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
            // Set the GPIO modes
            Pi.Gpio.Pin07.PinMode = GpioPinDriveMode.Output;
            Pi.Gpio.Pin08.PinMode = GpioPinDriveMode.Output;
            Pi.Gpio.Pin09.PinMode = GpioPinDriveMode.Output;
            Pi.Gpio.Pin10.PinMode = GpioPinDriveMode.Output;
        }

        private static void LeftMotor(bool forwards)
        {
            Pi.Gpio.Pin07.PinMode = GpioPinDriveMode.Output;
            Pi.Gpio.Pin08.PinMode = GpioPinDriveMode.Output;
            //Turn the left motor forwards
            Pi.Gpio.Pin07.Write(!forwards);
            Pi.Gpio.Pin08.Write(forwards);
        }

        private static void TurnMotorsOff()
        {
            //Turn all motors off
            Pi.Gpio.Pin07.Write(false);
            Pi.Gpio.Pin08.Write(false);
            Pi.Gpio.Pin09.Write(false);
            Pi.Gpio.Pin10.Write(false);
        }

        private static void RightMotor(bool forwards)
        {
            Pi.Gpio.Pin09.PinMode = GpioPinDriveMode.Output;
            Pi.Gpio.Pin10.PinMode = GpioPinDriveMode.Output;
            //Turn the right motor forwards
            Pi.Gpio.Pin09.Write(!forwards);
            Pi.Gpio.Pin10.Write(forwards);
        }
    }
}
