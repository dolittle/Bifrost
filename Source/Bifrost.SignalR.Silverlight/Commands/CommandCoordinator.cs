using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Bifrost.Commands;
using Bifrost.Domain;
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

        public CommandCoordinator(ISerializer serializer, IHubProxyManager hubProxyFactory)
        {
            _serializer = serializer;

            _proxy = hubProxyFactory.Get<Proxies.ICommandCoordinator>();
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
            commandResult.ValidationResults = validationResults;
            commandResult.CommandValidationMessages = commandValidationMessages;

            var descriptor = new CommandDescriptor
            {
                Name = command.Name,
                Command = _serializer.ToJson(command.Parameters)
            };
            command.IsBusy = true;
            _proxy.Handle(descriptor).ContinueWith(a =>
            {
                foreach (var commandValidationMessage in a.Result.CommandValidationMessages)
                    commandValidationMessages.Add(commandValidationMessage);
                foreach (var validationResult in a.Result.ValidationResults)
                    validationResults.Add(validationResult);

                commandResult.Exception = a.Result.Exception;

                command.IsBusy = false;
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
