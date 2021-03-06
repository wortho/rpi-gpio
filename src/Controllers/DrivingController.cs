using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

         [HttpGet("ws")]
        public async Task GetWS()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                Distance.MonitorDistance(logger, CancellationToken.None, Driving.CheckDistance);
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                logger.LogInformation($"WebSocket State:{webSocket.State}");
                await SendStatus(HttpContext, webSocket);
            }
            else
            {                
                throw new HttpRequestException();
            }
        }

        private async Task SendStatus(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var resultTask = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!resultTask.IsCompleted && webSocket.State == WebSocketState.Open)
            {
                var response = string.Format($"Time: {System.DateTime.Now}, State: {Driving.State}, Speed:{Driving.Speed}, Distance: {Distance.CurrentDistance}");
                var bytes = System.Text.Encoding.UTF8.GetBytes(response);
                await webSocket.SendAsync(
                    new System.ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);    
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            var result = await resultTask;
            if (result != null && result.CloseStatus.HasValue)
            {
                logger.LogInformation($"Received Result: {result.MessageType}");
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);                
            }
            logger.LogInformation($"WebSocket State:{webSocket.State}");
        }
    }

}