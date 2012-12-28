using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Dynamic;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Bifrost.SignalR.Silverlight.Hubs;
using SignalR.Client.Hubs;

namespace Bifrost.SignalR.Silverlight.Commands
{
    public class CommandCoordinator : ICommandCoordinator
    {
        IHubProxy _hub;
        ISerializer _serializer;
        Proxies.ICommandCoordinator _proxy;
        Dictionary<Guid, ICommand> _commands = new Dictionary<Guid, ICommand>();

        public CommandCoordinator(ISerializer serializer, IHubProxyManager hubProxyFactory)
        {
            _serializer = serializer;

            _proxy = hubProxyFactory.Get<Proxies.ICommandCoordinator>();
            _proxy.EventsProcessed += EventsProcessed;
        }

        void EventsProcessed(Guid commandContext)
        {
            if (_commands.ContainsKey(commandContext))
            {
                var command = _commands[commandContext];
                command.IsProcessing = false;

                if (command is INotifyEventsProcessed)
                    ((INotifyEventsProcessed)command).OnEventsProcessed(commandContext);

                _commands.Remove(commandContext);
            }
        }

        public CommandResult Handle(ISaga saga, ICommand command)
        {
            throw new System.NotImplementedException();
        }

        public CommandResult Handle(ICommand command)
        {
            var validationResults = new ObservableCollection<ValidationResult>();
            var commandValidationMessages = new ObservableCollection<string>();
            var commandResult = new CommandResult();
            commandResult.CommandName = command.Name;
            commandResult.ValidationResults = validationResults;
            commandResult.CommandValidationMessages = commandValidationMessages;

            if( !command.GetType().Equals(typeof(Command)) )
                CopyPropertyValuesToParameters(command);

            var descriptor = new CommandDescriptor
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Command = _serializer.ToJson(command.Parameters)
            };
            _commands[descriptor.Id] = command;
            command.IsBusy = true;
            command.IsProcessing = true;
            _proxy.Handle(descriptor).ContinueWith(a =>
            {
                foreach (var commandValidationMessage in a.Result.CommandValidationMessages)
                    commandValidationMessages.Add(commandValidationMessage);
                foreach (var validationResult in a.Result.ValidationResults)
                    validationResults.Add(validationResult);

                commandResult.Exception = a.Result.Exception;
                commandResult.ExceptionMessage = a.Result.ExceptionMessage;

                command.IsBusy = false;

                if( command is INotifyCommandResultsReceived ) 
                    ((INotifyCommandResultsReceived)command).OnCommandResultsReceived(descriptor.Id, commandResult);
            });

            return commandResult;
        }

        private static void CopyPropertyValuesToParameters(ICommand command)
        {
            var parameters = command.Parameters as BindableExpandoObject;
            var commandType = command.GetType();
            var properties = commandType.GetProperties().Where(p => p.DeclaringType == commandType);
            foreach (var property in properties )
            {
                var value = property.GetValue(command, null);
                parameters[property.Name] = value;
            }
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
