﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Webserver.Models;

namespace Webserver.Hubs
{
    public class WhiskyHub : Hub
    {
        protected IHubContext<WhiskyHub> _context;
        public WhiskyHub(IHubContext<WhiskyHub> context)
        {
            _context = context;
        }

        public async Task AddMeasurement(Measurement measurement)
        {
            await _context.Clients.All.SendAsync("MeasurementAdded", measurement);
        }
    }
}
