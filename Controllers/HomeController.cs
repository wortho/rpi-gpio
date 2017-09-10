using rpi.gpio.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace rpi.gpio.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        
        private readonly ILogger logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public Status Get()
        {
            logger.LogInformation("HomeController Get called");
            return new Status("Welcome to Pi");
        }
    }
}
