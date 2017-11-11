using rpi.gpio.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace rpi.gpio.Controllers
{
    [Route("api/[controller]")]
    public class DistanceController : Controller
    {
        private readonly ILogger logger;
        public DistanceController(ILogger<DistanceController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));            
        }

        [HttpGet]
        public decimal Get() => Distance.MeasureDistance(logger);
    }
}
