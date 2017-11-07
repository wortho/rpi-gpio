using rpi.gpio.Model;
using Microsoft.AspNetCore.Mvc;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Computer;

namespace rpi.gpio.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        [HttpGet]
        public SystemInfo Get()
        {
            return Pi.Info;
        }
    }
}
