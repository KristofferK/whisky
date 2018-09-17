using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
        private IMongoCollection<BsonDocument> collection;
        public Mongo(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("whisky_measurements");
            collection = database.GetCollection<BsonDocument>("measurements");
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
            var filter = Builders<BsonDocument>.Filter.Gt("DateMeasured", dateTime);
            var cursor = collection.Find(filter).SortBy(e => e["DateMeasured"]).ToCursor();
            return cursor.ToEnumerable().Select(e => new Models.Measurement()
            {
                Pressure = e["Pressure"].AsInt32,
                SensorID = e["SensorID"].AsString,
                Temperature = e["Temperature"].AsDouble,
                DateMeasured = e["DateMeasured"].ToUniversalTime()
            });
        }

        public void Insert(Models.Measurement measurement)
        {
            collection.InsertOneAsync(measurement.ToBsonDocument());
        }
    }
}
