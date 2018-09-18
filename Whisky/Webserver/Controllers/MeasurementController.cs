using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
        private readonly IHubContext<WhiskyHub> hubContext;
        private static IMeasurementPersistence persistency;

        static MeasurementController()
        {
            persistency = new Mongo(Startup.ServerConfiguration.MongoConnectionString);
        }

        public MeasurementController(IHubContext<WhiskyHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPut("[action]")]
        public JsonResult Add([FromBody] Measurement measurement, [FromHeader] string authorization)
        {
            var authorized = GetAuthorization(authorization);
            if (!authorized.IsAuthorized)
            {
                return Json(new { Status = "Error", Message = authorized.Message });
            }

            measurement.DateMeasured = DateTime.Now;
            hubContext.Clients.All.SendAsync("MeasurementAdded", measurement);
            persistency.Insert(measurement);
            return Json(new { Status = "Added", Username = authorized.Username, MeasurementAdded = measurement });
        }

        [HttpGet("[action]")]
        public IEnumerable<Measurement> GetExistingMeasurements()
        {
            return persistency.GetLatest(50);
        }

        private ApiAuthorizationResponse GetAuthorization(string authorization)
        {
            if (authorization == null || !authorization.StartsWith("Basic "))
            {
                return new ApiAuthorizationResponse("Authorization failed: Please specify a basic authorization header in your request");
            }

            var tokenEncoded = authorization.Substring(6);
            var token = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(tokenEncoded));

            var colonIndex = token.IndexOf(':');
            if (colonIndex == -1)
            {
                return new ApiAuthorizationResponse("Authorization failed: Please seperate username and password with a colon");
            }

            var username = token.Substring(0, colonIndex);
            var password = token.Substring(colonIndex + 1);

            if (username != Startup.ServerConfiguration.ApiUsername || password != Startup.ServerConfiguration.ApiPassword)
            {
                return new ApiAuthorizationResponse("Authorization failed: Invalid credentials");
            }

            return new ApiAuthorizationResponse(null)
            {
                IsAuthorized = true,
                Username = username
            };
        }
    }
}
