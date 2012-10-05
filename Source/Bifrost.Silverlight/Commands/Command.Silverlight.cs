using System;

namespace Bifrost.Commands
{
    public partial class Command : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
            var i = 0;
            i++;
            
        }
    }
}
