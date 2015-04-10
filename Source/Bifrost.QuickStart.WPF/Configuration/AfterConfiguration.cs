using Bifrost.Configuration;

namespace Bifrost.QuickStart.WPF.Configuration
{
    public class AfterConfiguration : IWantToKnowWhenConfigurationIsDone
    {
        public void Configured(IConfigure configure)
        {
            var i = 0;
            i++;
        }
    }
}
