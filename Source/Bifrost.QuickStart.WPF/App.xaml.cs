using System.Windows;
using Bifrost.Configuration;
using Bifrost.Configuration.Assemblies;

namespace Bifrost.QuickStart.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        static App()
        {
            Configure.DiscoverAndConfigure(a => a.IncludeAll()); //.ExceptAssembliesStartingWith("System","Microsoft","mscor","FluentValidation","Ninject"));
        }
    }
}
