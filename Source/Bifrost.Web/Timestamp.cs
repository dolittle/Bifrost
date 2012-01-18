using System;

namespace Bifrost.Web
{
    public class Timestamp
    {
        public virtual DateTime Value { get; set; }
        public static implicit operator Timestamp(DateTime dateTime)
        {
            return new Timestamp() { Value = dateTime };
        }
        public static implicit operator DateTime(Timestamp timestamp)
        {
            return timestamp == null ? new DateTime(1800, 1, 1) : timestamp.Value;
        }
    }
}