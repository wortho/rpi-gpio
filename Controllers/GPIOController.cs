using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Controllers
{
    [Route("api/[controller]")]
    public class GPIOController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var status = new List<string>();
            foreach (var pin in Pi.Gpio)
            {
                 if (pin.Capabilities.Contains(PinCapability.GP))
                 {
                    pin.PinMode = GpioPinDriveMode.Input;
                    status.Add($"{pin.Name} {pin.Read()}");
                 }
            }
            return status;
        }

        [HttpGet("{id}")]
        public bool Get(int id)
        {
            var pin = Pi.Gpio[id];
            pin.PinMode = GpioPinDriveMode.Input;
            return pin.Read();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]bool value)
        {
            var pin = Pi.Gpio[id];
            pin.PinMode = GpioPinDriveMode.Output;
            pin.Write(value);
        }
    }
}
