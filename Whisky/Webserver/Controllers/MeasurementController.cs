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
            return persistency.GetLatest(DateTime.Now.AddMonths(-1));
        }
    }
}
