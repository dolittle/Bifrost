using System.Reflection;
using System.Windows;
using Bifrost.Configuration;
using Bifrost.Configuration.AssemblyLocator;

namespace Bifrost.QuickStart.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static App()
        {
            Configure.DiscoverAndConfigure(a => a.IncludeAll().ExceptAssembliesStartingWith("Bifrost"));
        }
    }
}
