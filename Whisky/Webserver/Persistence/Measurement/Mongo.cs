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
            var cursor = collection
                .Find(e => true)
                .SortByDescending(e => e["DateMeasured"])
                .Limit(limit)
                .SortBy(e => e["DateMeasured"])
                .ToCursor();

            return cursor.ToEnumerable().Select(GenerateMeasurement);
        }

        public IEnumerable<Models.Measurement> GetLatest(string sensorID, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Measurement> GetLatest(DateTime dateTime)
        {
            var filter = Builders<BsonDocument>.Filter.Gt("DateMeasured", dateTime);
            var cursor = collection.Find(filter).SortBy(e => e["DateMeasured"]).ToCursor();
            return cursor.ToEnumerable().Select(GenerateMeasurement);
        }

        public void Insert(Models.Measurement measurement)
        {
            collection.InsertOneAsync(measurement.ToBsonDocument());
        }

        private Models.Measurement GenerateMeasurement(BsonDocument bson)
        {
            return new Models.Measurement()
            {
                Pressure = bson["Pressure"].AsInt32,
                SensorID = bson["SensorID"].AsString,
                Temperature = bson["Temperature"].AsDouble,
                DateMeasured = bson["DateMeasured"].ToUniversalTime()
            };
        }
    }
}
