using System;
using System.ComponentModel;
using Bifrost.Dynamic;
using Bifrost.Extensions;

namespace Bifrost.Commands
{
    public partial class Command : INotifyPropertyChanged
    {
        dynamic _parameters;

        /// <summary>
        /// Initializes a new instance of <see cref="Command"/>
        /// </summary>
        /// <param name="commandCoordinator"><see cref="ICommandCoordinator"/> to use for handling the command</param>
        public Command(ICommandCoordinator commandCoordinator) : base()
        {
            CommandCoordinator = commandCoordinator;
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

        bool _isProcessing;
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                _isProcessing = value;
                PropertyChanged.Notify(() => IsProcessing);
            }
        }

        public ICommandCoordinator CommandCoordinator { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            if( CommandCoordinator != null ) 
                CommandCoordinator.Handle(this);
        }


#pragma warning restore 1591 // Xml Comments
    }
}
