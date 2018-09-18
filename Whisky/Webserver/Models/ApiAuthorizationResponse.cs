using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webserver.Models
{
    public class ApiAuthorizationResponse
    {
        public bool IsAuthorized { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public ApiAuthorizationResponse(string message)
        {
            Message = message;
        }
    }
}
