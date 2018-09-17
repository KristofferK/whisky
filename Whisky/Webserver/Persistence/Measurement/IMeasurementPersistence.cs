using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Persistence.Measurement
{
    public interface IMeasurementPersistence
    {
        void Insert(Models.Measurement measurement);
        IEnumerable<Models.Measurement> GetLatest(int limit);
        IEnumerable<Models.Measurement> GetLatest(string sensorID, int limit);
        IEnumerable<Models.Measurement> GetLatest(DateTime dateTime);
        // IEnumerable<Models.Measurement> GetAbnormalities();
    }
}
