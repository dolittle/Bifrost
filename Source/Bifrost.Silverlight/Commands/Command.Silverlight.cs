using System;
using System.Dynamic;

namespace Bifrost.Commands
{
    public partial class Command : System.Windows.Input.ICommand
    {
        dynamic _parameters;
        ICommandCoordinator _commandCoordinator;


        public string Name { get; set; }
        public dynamic Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new ExpandoObject();
                return _parameters;
            }
        }

        public static Command Create(ICommandCoordinator commandCoordinator,string name)
        {
            var command = new Command
            {
                Name = name,
                _commandCoordinator = commandCoordinator,
            };
            return command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
            _commandCoordinator.Handle(this);
        }
    }
}
