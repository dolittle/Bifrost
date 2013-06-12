using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Web.Specs.Proxies.for_ReadModelProxyExtensions
{
    class ObjectWithString
    {
        public string StringProperty { get; set; }
    }

    class ObjectWithInt32
    {
        public int Int32Property { get; set; }
    }

    class ObjectWithDateTime
    {
        public DateTime DateTimeProperty { get; set; }
    }

    class ObjectWithDateTimeNullable
    {
        public DateTime? DateTimeNullableProperty { get; set; }
    }

    class ObjectWithBoolean
    {
        public bool BooleanProperty { get; set; }
    }
}
