using System.Windows;
using Bifrost.Configuration;

namespace Bifrost.QuickStart.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            Configure.DiscoverAndConfigure();
        }
    }
}
