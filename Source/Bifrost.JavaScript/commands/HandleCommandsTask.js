Bifrost.namespace("Bifrost.commands", {
    HandleCommandsTask: Bifrost.tasks.ExecutionTask.extend(function (commands, server) {
        /// <summary>Represents a task that can handle an array of command</summary>
        var self = this;

        var scriptSource = (function () {
            var script = $("script[src*='Bifrost/Application']").get(0);

            if (script.getAttribute.length !== undefined) {
                return script.src;
            }

            return script.getAttribute('src', -1);
        }());

		var uri = Bifrost.Uri.create(scriptSource);

		var port = uri.port || "";
		if (!Bifrost.isUndefined(port) && port !== "" && port !== 80) {
			port = ":" + port;
		}

		this.origin = uri.scheme + "://" + uri.host + port;
        
        this.names = [];
        commands.forEach(function (command) {
            self.names.push(command.name);
        });

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var commandDescriptors = [];

            commands.forEach(function (command) {
                command.isBusy(true);
                var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
                commandDescriptors.push(commandDescriptor);
            });

            var parameters = {
                commandDescriptors: commandDescriptors
            };

            var url = self.origin + "/Bifrost/CommandCoordinator/HandleMany";

            server.post(url, parameters).continueWith(function (results) {
                var commandResults = [];

                results.forEach(function (result) {
                    var commandResult = Bifrost.commands.CommandResult.createFrom(result);
                    commandResults.push(commandResult);
                });
                promise.signal(commandResults);
            });

            return promise;
        };
    })
});