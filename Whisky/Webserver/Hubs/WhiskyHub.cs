using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Webserver.Models;

namespace Webserver.Hubs
{
    public class WhiskyHub : Hub
    {
        public async Task AddMeasurement(Measurement measurement)
        {
            await Clients.All.SendAsync("MeasurementAdded", measurement);
        }
    }
}
