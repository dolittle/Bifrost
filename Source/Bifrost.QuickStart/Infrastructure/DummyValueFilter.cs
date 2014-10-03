using Bifrost.Web.Services;

namespace Web.Infrastructure
{
    public class DummyValueFilter : ICanInterceptValue<string> 
    {
        public string Intercept(string value)
        {
            return value.Replace("Foo", "Bar");
        }
    }
}