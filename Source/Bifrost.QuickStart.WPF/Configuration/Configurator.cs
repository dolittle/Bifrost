using Bifrost.Configuration;

namespace Bifrost.QuickStart.WPF.Configuration
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            configure.Frontend.Desktop();
        }
    }
}
