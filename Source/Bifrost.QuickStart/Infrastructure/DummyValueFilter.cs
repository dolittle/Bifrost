using Bifrost.Web.Services;

namespace Bifrost.QuickStart.Infrastructure
{
    public class DummyValueFilter : ICanInterceptValue<string> 
    {
        public string Intercept(string value)
        {
            return value.Replace("Foo", "Bar");
        }
    }
}