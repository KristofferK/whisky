using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Models
{
    public class Configuration
    {
        public string MongoConnectionString { get; set; } = "mongodb://localhost:27017/<DATABASE>";
        public string ApiUsername { get; set; } = "username";
        public string ApiPassword { get; set; } = "password";
    }
}
