using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rpi.gpio.Model;

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
            Motors.TurnMotorsOff();
            logger.LogInformation(Motors.GetStatus(Motors.LeftMotorForwardsPin));
            logger.LogInformation(Motors.GetStatus(Motors.LeftMotorReversePin));
            logger.LogInformation(Motors.GetStatus(Motors.RightMotorReversePin));
            logger.LogInformation(Motors.GetStatus(Motors.RightMotorForwardsPin));
        }

        [HttpGet]
        public string[] GetMotors()
        {
            return Motors.GetMotorPinStatus();
        }


        [HttpPut("{id}")]
        public void PutMotor(int id, [FromBody] bool forward)
        {
            switch (id)
            {
                case 0:
                    Motors.LeftMotor(forward);
                    break;
                case 1:
                    Motors.RightMotor(forward);
                    break;
                default:
                    break;
            }
        }
    }
}
