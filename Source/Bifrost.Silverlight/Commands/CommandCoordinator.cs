using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows;
using System.Text;
using Bifrost.Serialization;
using System.Dynamic;

namespace Bifrost.Commands
{
    public class CommandCoordinator : ICommandCoordinator
    {
        ISerializer _serializer;

        public CommandCoordinator(ISerializer serializer)
        {
            _serializer = serializer;
        }


        public CommandResult Handle(Sagas.ISaga saga, ICommand command)
        {
            throw new NotImplementedException();
        }

        public CommandResult Handle(ICommand command)
        {
            var validationResult = new ObservableCollection<ValidationResult>();
            var commandValidationMessages = new ObservableCollection<string>();
            var commandResult = new CommandResult();
            commandResult.ValidationResults = validationResult;
            commandResult.CommandValidationMessages = commandValidationMessages;

            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;

            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}/CommandCoordinator/Handle",
                source.Scheme,
                source.Host,
                source.Port == 80 ? string.Empty : ":" + source.Port);

            dynamic parameters = new ExpandoObject();
            dynamic commandDescriptor = new ExpandoObject();
            commandDescriptor.Name = "TestCommand";
            commandDescriptor.Command = _serializer.ToJson(new { });

            parameters.commandDescriptor = _serializer.ToJson(commandDescriptor);

            var json = _serializer.ToJson(parameters);

            
            var uri = new Uri(url, UriKind.Absolute);
            client.UploadStringAsync(uri, "POST", json);
            client.UploadStringCompleted += (s, e) =>
                {
                    var actualCommandResult = _serializer.FromJson<CommandResult>(e.Result);
                    var i = 0;
                    i++;
                };
            return commandResult;
        }


        public CommandResult Handle<T>(Sagas.ISaga saga, Guid aggregatedRootId, System.Linq.Expressions.Expression<Action<T>> method) where T : Domain.AggregatedRoot
        {
            throw new NotImplementedException();
        }

        public CommandResult Handle<T>(Guid aggregatedRootId, System.Linq.Expressions.Expression<Action<T>> method) where T : Domain.AggregatedRoot
        {
            throw new NotImplementedException();
        }
    }
}
