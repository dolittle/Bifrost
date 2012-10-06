using System;
using System.Linq.Expressions;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Sagas;
using SignalR.Client;
using System.Windows;
using SignalR.Client.Hubs;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Bifrost.Serialization;
using System.Dynamic;

namespace Bifrost.SignalR.Silverlight.Commands
{
    public class CommandCoordinator : ICommandCoordinator
    {
        HubConnection _connection;
        IHubProxy _hub;
        ISerializer _serializer;

        public CommandCoordinator(ISerializer serializer)
        {
            _serializer = serializer;

            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}",
                source.Scheme,
                source.Host,
                source.Port == 80 ? string.Empty : ":" + source.Port);

            _connection = new HubConnection(url);

            _hub = _connection.CreateProxy("CommandCoordinator");

            _connection.Start().Wait();
        }

        public CommandResult Handle(ISaga saga, ICommand command)
        {
            throw new System.NotImplementedException();
        }

        public CommandResult Handle(ICommand command)
        {
            var validationResult = new ObservableCollection<ValidationResult>();
            var commandValidationMessages = new ObservableCollection<string>();
            var commandResult = new CommandResult();
            commandResult.ValidationResults = validationResult;
            commandResult.CommandValidationMessages = commandValidationMessages;

            dynamic parameters = new ExpandoObject();
            dynamic commandDescriptor = new ExpandoObject();
            commandDescriptor.Name = "TestCommand";
            //commandDescriptor.Command = _serializer.ToJson(new { });

            parameters.commandDescriptor = _serializer.ToJson(commandDescriptor);

            var json = (string)_serializer.ToJson(parameters);


            _hub.Invoke<CommandResult>("Handle", json).ContinueWith(a =>
            {
                var i = 0;
                i++;
            });

            return commandResult;
        }

        public CommandResult Handle<T>(ISaga saga, Guid aggregatedRootId, Expression<System.Action<T>> method) where T : AggregatedRoot
        {
            throw new System.NotImplementedException();
        }

        public CommandResult Handle<T>(Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot
        {
            throw new System.NotImplementedException();
        }
    }
}
