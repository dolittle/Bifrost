using System;
using Bifrost.Commands;

namespace Bifrost.Client.Specs.Commands.for_CommandInvocationHandler
{
    public class Command : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public ICommand Instance { get; set; }
    }
}
