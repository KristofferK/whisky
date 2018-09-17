using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webserver.Models;

namespace Webserver.Persistence.Measurement
{
    public class Mongo : IMeasurementPersistence
    {
        private MongoClient client;
        public Mongo(string connectionString)
        {
            client = new MongoClient(connectionString);
        }

        public IEnumerable<Models.Measurement> GetLatest(int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Measurement> GetLatest(int sensorID, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Measurement> GetLatest(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void Insert(Models.Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
