using System.ComponentModel;
using Bifrost.Commands;
using Bifrost.QuickStart.Domain.HumanResources.Employees;
using Bifrost.Values;
using ICommand = System.Windows.Input.ICommand;

namespace Bifrost.QuickStart.WPF
{
    public class SomeCommand : Command
    {

    }

    [NotifyChanges]
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ICommandFor<RegisterEmployee> register, ICommandFor<SomeCommand> cmd)
        {
            Register = register;
            register.Succeeded((c, r) =>
            {
            });

            register.Failed((c, r) =>
            {
                var i = 0;
                i++;
            });

            cmd.Succeeded((c, r) =>
            {
                var i = 0;
                i++;
            });

            cmd.Failed((c, r) =>
            {
                var i = 0;
                i++;
            });
            

            Instance = register.Instance;
            Instance.FirstName = "Einar";
        }

        public virtual ICommand Register { get; private set; }

        public virtual RegisterEmployee Instance { get; private set; }


        public void DoStuff()
        {
            var type = this.GetType();
            var i = 0;
            i++;
        }
    }
}
