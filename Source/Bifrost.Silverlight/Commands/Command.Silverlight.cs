using System;
using System.ComponentModel;
using Bifrost.Dynamic;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    public partial class Command : INotifyPropertyChanged
    {
        ICommandCoordinator _commandCoordinator;
        dynamic _parameters;

        /// <summary>
        /// Initializes a new instance of <see cref="Command"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use for handling the command</param>
        public Command(ICommandCoordinator commandCoordinator) : base()
        {
            _commandCoordinator = commandCoordinator;
        }

#pragma warning disable 1591 // Xml Comments
        public event EventHandler CanExecuteChanged = (s, e) => { };
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public string Name { get; set; }
        public dynamic Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new BindableExpandoObject();
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

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            if( _commandCoordinator != null ) 
                _commandCoordinator.Handle(this);
        }


#pragma warning restore 1591 // Xml Comments
    }
}
