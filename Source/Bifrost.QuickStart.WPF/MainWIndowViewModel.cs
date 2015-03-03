using System.ComponentModel;
using Bifrost.Commands;
using Bifrost.QuickStart.Domain.HumanResources.Employees;

namespace Bifrost.QuickStart.WPF
{
    public class MainWindowViewModel
    {
        public class Something : INotifyDataErrorInfo
        {

            public string Hello { get; set; }

            public event System.EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

            public System.Collections.IEnumerable GetErrors(string propertyName)
            {
                return new[] { "Its just wrong!", "And more stuff is wrong as well!" };
            }

            public bool HasErrors
            {
                get { return true; }
            }
        }



        public MainWindowViewModel(ICommandFor<RegisterEmployee> register)
        {
            Register = register;

            Stuff = new Something();

            Instance = register.Instance;
            Instance.FirstName = "Einar";
        }

        public Something Stuff { get; private set; }

        public ICommand Register { get; private set; }

        public RegisterEmployee Instance { get; private set; }


        public void DoStuff()
        {
            var i = 0;
            i++;
        }
    }
}
