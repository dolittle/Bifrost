Bifrost.namespace("Bifrost.commands", {
    HandleCommandTask: Bifrost.tasks.ExecutionTask.extend(function (command, server) {
        /// <summary>Represents a task that can handle a command</summary>
        this.name = command.name;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
            var parameters = {
                commandDescriptor: commandDescriptor
            };

            var url = "/Bifrost/CommandCoordinator/Handle?_cmd=" + command.generatedFrom;

            server.post(url, parameters).continueWith(function (result) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(result);
                promise.signal(commandResult);
            });

            return promise;
        };
    })
});