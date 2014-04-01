using Bifrost.Web.Services;

namespace Bifrost.QuickStart.Infrastructure
{
    public class DummyValueFilter : IInputValueFilter 
    {
        public string Filter(string value)
        {
            if (value == null)
            {
                return value;
            }

            return value.Replace("Foo", "Bar");
        }
    }
}