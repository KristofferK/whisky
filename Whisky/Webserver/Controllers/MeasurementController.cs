using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Webserver.Hubs;
using Webserver.Models;
using Webserver.Persistence.Measurement;

namespace Webserver.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementController : Controller
    {
        private readonly IHubContext<WhiskyHub> _hubContext;
        private static IMeasurementPersistence persistency;

        static MeasurementController()
        {
            persistency = new Mongo(ConnectionStrings.MONGO_CONNECTION_STRING);
        }

        public MeasurementController(IHubContext<WhiskyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPut("[action]")]
        public JsonResult Add([FromBody] Measurement measurement)
        {
            measurement.DateMeasured = DateTime.Now;
            _hubContext.Clients.All.SendAsync("MeasurementAdded", measurement);
            persistency.Insert(measurement);
            return Json(new { Message = "Added", MeasurementAdded = measurement });
        }

        [HttpGet("[action]")]
        public IEnumerable<Measurement> GetExistingMeasurements()
        {
            return new List<Measurement>()
            {
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 0, 0),
                    Pressure = 1200,
                    SensorID = "1",
                    Temperature = 40.2
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 0, 0),
                    Pressure = 1202,
                    SensorID = "2",
                    Temperature = 40.3
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 0, 0),
                    Pressure = 1201,
                    SensorID = "3",
                    Temperature = 40.1
                },

                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 5, 0),
                    Pressure = 1201,
                    SensorID = "1",
                    Temperature = 44.9
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 5, 0),
                    Pressure = 1202,
                    SensorID = "2",
                    Temperature = 45
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 5, 0),
                    Pressure = 1200,
                    SensorID = "3",
                    Temperature = 45.1
                },

                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 10, 0),
                    Pressure = 1210,
                    SensorID = "1",
                    Temperature = 49.1
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 10, 0),
                    Pressure = 1210,
                    SensorID = "2",
                    Temperature = 50.2
                },
                new Measurement()
                {
                    DateMeasured = new DateTime(2018, 9, 16, 18, 10, 0),
                    Pressure = 1209,
                    SensorID = "3",
                    Temperature = 50
                },
            };
        }
    }
}
