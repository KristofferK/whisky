using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Models
{
    public class Measurement
    {
        /// <summary>
        /// Temperature in celsius
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Pressure in millibar
        /// </summary>
        public int Pressure { get; set; }
        /// <summary>
        /// Id of the sensor that measured this measurement.
        /// </summary>
        public string SensorID { get; set; }
        /// <summary>
        /// Date of the measurement
        /// </summary>
        public DateTime DateMeasured { get; set; }
    }
}
