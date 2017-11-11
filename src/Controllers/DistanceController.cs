using rpi.gpio.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        public decimal Get()
        {
            Distance.MonitorDistance(logger, CancellationToken.None);
            return Distance.CurrentDistance;
        } 
    }
}
