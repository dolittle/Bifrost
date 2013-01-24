using System.Collections.Generic;

namespace Bifrost.Web.Read
{
    public class QueryDescriptor
    {
        public string NameOfQuery { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
