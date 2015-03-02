using Bifrost.Commands;
using Bifrost.QuickStart.Domain.HumanResources.Employees;

namespace Bifrost.QuickStart.WPF
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ICommandFor<RegisterEmployee> register)
        {
            Register = register;
        }

        public ICommand Register { get; private set; }
    }
}
