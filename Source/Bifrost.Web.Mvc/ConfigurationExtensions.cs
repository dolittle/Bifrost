using System.Web.Mvc;
using Bifrost.Web.Mvc.Views;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingKnockout(this IConfigure configuration)
        {
            ViewEngines.Engines.Add(new KnockoutRazorViewEngine());
            return Configure.Instance;
        }
    }
}
