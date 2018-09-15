using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webserver.Models;

namespace Webserver.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementController : Controller
    {
        [HttpPut("[action]")]
        public JsonResult Add([FromBody] Measurement content)
        {
            return Json(new { Message = "Added", PayloadReceived = content });
        }
    }
}
