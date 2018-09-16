using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Webserver.Hubs;
using Webserver.Models;

namespace Webserver.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementController : Controller
    {
        private readonly IHubContext<WhiskyHub> _hubContext;

        public MeasurementController(IHubContext<WhiskyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPut("[action]")]
        public JsonResult Add([FromBody] Measurement content)
        {
            var measurement = new MeasurementCelsius()
            {
                Pressure = content.Pressure,
                Temperature = content.Temperature / 10 - 273.15,
                SensorID = content.SensorID
            };
            _hubContext.Clients.All.SendAsync("MeasurementAdded", measurement);
            return Json(new { Message = "Added", PayloadReceived = content, MeasurementAdded = measurement });
        }
    }
}
