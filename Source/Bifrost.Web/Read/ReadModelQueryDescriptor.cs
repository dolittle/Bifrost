using System.Collections.Generic;

namespace Bifrost.Web.Read
{
    public class ReadModelQueryDescriptor
    {
        public string ReadModel { get; set; }
        public Dictionary<string, object> PropertyFilters { get; set; }
    }
}
