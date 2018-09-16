using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Models
{
    public class MeasurementCelsius
    {
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public string SensorID { get; set; }
    }
}
