using Bifrost.Commands;
using Bifrost.QuickStart.Domain.HumanResources.Employees;

namespace Bifrost.QuickStart.WPF
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ICommandFor<RegisterEmployee> register, ICommandFor<RegisterEmployee> register2)
        {
            Register = register;

            var o = (object)register;
            
            Instance = register.Instance;
            Instance.FirstName = "Einar";
        }

        public ICommand Register { get; private set; }


        public RegisterEmployee Instance { get; private set; }


        public void DoStuff()
        {
            var i = 0;
            i++;
        }
    }
}
