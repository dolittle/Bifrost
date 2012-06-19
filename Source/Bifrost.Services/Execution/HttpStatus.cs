using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Services.Execution
{
    public class HttpStatus
    {
        public class HttpStatusException : Exception
        {
            public HttpStatusException(int code, string description)
            {
                Code = code;
                Description = description;
            }

            public int Code { get; private set; }
            public string Description { get; private set; }
        }

        public static void NotFound(string description = "Not found")
        {
            throw new HttpStatusException(404, description);
        }
    }
}
