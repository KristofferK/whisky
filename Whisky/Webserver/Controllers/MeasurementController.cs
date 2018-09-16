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
                Temperature = Math.Round(content.Temperature / 10 - 273.15, 2),
                SensorID = content.SensorID,
                DateMeasured = DateTime.Now
            };
            _hubContext.Clients.All.SendAsync("MeasurementAdded", measurement);
            return Json(new { Message = "Added", PayloadReceived = content, MeasurementAdded = measurement });
        }

        [HttpGet("[action]")]
        public IEnumerable<MeasurementCelsius> GetExistingMeasurements()
        {
            return new List<MeasurementCelsius>()
            {
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 0, 0),
                    Pressure = 1200,
                    SensorID = "1",
                    Temperature = 40.2
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 0, 0),
                    Pressure = 1202,
                    SensorID = "2",
                    Temperature = 40.3
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 0, 0),
                    Pressure = 1201,
                    SensorID = "3",
                    Temperature = 40.1
                },

                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 5, 0),
                    Pressure = 1201,
                    SensorID = "1",
                    Temperature = 44.9
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 5, 0),
                    Pressure = 1202,
                    SensorID = "2",
                    Temperature = 45
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 5, 0),
                    Pressure = 1200,
                    SensorID = "3",
                    Temperature = 45.1
                },

                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 10, 0),
                    Pressure = 1210,
                    SensorID = "1",
                    Temperature = 49.1
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 10, 0),
                    Pressure = 1210,
                    SensorID = "2",
                    Temperature = 50.2
                },
                new MeasurementCelsius()
                {
                    DateMeasured = new DateTime(2018, 9, 6, 18, 10, 0),
                    Pressure = 1209,
                    SensorID = "3",
                    Temperature = 50
                },
            };
        }
    }
}
