using System;
using System.ComponentModel;
using System.Dynamic;
using Bifrost.Dynamic;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    public partial class Command : INotifyPropertyChanged
    {
        ICommandCoordinator _commandCoordinator;
        dynamic _parameters;

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

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                PropertyChanged.Notify(() => IsBusy);
            }
        }

        public static Command Create(ICommandCoordinator commandCoordinator,string name, object initialParameterValues = null)
        {
            var command = new Command
            {
                Name = name,
                _commandCoordinator = commandCoordinator,
            };

            if( initialParameterValues != null ) 
                DynamicHelpers.Populate(command.Parameters, initialParameterValues);

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

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
    }
}
