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
    public class DrivingController : Controller
    {
        private readonly ILogger logger;

        public DrivingController(ILogger<MotorController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("forward")]
        public IActionResult Forward([FromBody] int speed = 10)
        {
            Driving.Forward(speed);
            return new AcceptedResult();
        }
       
        [HttpPost("left")]
        public IActionResult Left([FromBody] int speed = 10)
        {
            Driving.Left(speed);
            return new AcceptedResult();
        }

        [HttpPost("right")]
        public IActionResult Right([FromBody] int speed = 10)
        {
            Driving.Right(speed);
            return new AcceptedResult();
        }

        [HttpPost("reverse")]
        public IActionResult Reverse([FromBody] int speed = 10)
        {
            Driving.Reverse(speed);
            return new AcceptedResult();
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            Driving.Stop();
            return new AcceptedResult();
        }
    }

}