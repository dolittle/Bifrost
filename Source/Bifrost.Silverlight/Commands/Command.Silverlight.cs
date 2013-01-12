using System;
using System.ComponentModel;
using Bifrost.Dynamic;
using Bifrost.Extensions;
using System.Windows;

namespace Bifrost.Commands
{
    public partial class Command : INotifyPropertyChanged
    {
        dynamic _parameters;
        string _name;

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
        public event CommandResultsReceived CommandResultsReceived = (c, r) => { };
        public event EventsProcessed EventsProcessed = (p) => { };

        
        public string Name 
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    _name = GetType().Name;

                return _name;
            }
            set { _name = value; }
        }
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
            if (CommandCoordinator != null)
                CommandCoordinator.Handle(this);
        }


        public void OnCommandResultsReceived(Guid commandContextId, CommandResult result)
        {
            if (CommandResultsReceived != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => CommandResultsReceived(commandContextId, result));
        }

        public void OnEventsProcessed(Guid commandContextId)
        {
            if(EventsProcessed != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => EventsProcessed(commandContextId));
        }

#pragma warning restore 1591 // Xml Comments


    }
}
