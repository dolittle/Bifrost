using System.Collections.Specialized;
using System.Web;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Encapsulates a collection of parameters from the Http Request
    /// </summary>
    public class RequestParams : NameValueCollection
    {
        /// <summary>
        /// Adds a new Parameter to the collection
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        public override void Add(string name, string value)
        {
            if(this[name] == null)
                base.Add(name, value);
        }

        /// <summary>
        /// Converts an HttpCookieCollection into parameters
        /// </summary>
        /// <param name="cookies">An HttpCookieCollection</param>
        public void Add(HttpCookieCollection cookies)
        {
            var count = cookies.Count;
            for(var index = 0; index < count; index++)
            {
                var cookie = cookies.Get(index);
                Add(cookie.Name, cookie.Value);
            }
        }
    }
}