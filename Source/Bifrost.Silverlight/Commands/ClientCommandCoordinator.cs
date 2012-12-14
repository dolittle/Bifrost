using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Windows;
using Bifrost.Serialization;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandCoordinator"/> specialized for a client connecting to a server
    /// </summary>
    public class ClientCommandCoordinator : ICommandCoordinator
    {
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="ClientCommandCoordinator"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization back and forth to the server</param>
        public ClientCommandCoordinator(ISerializer serializer)
        {
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
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
#pragma warning restore 1591 // Xml Comments
}
