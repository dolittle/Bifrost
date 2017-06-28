Bifrost.namespace("Bifrost.commands", {
    HandleCommandTask: Bifrost.tasks.ExecutionTask.extend(function (command, server, systemEvents) {
        /// <summary>Represents a task that can handle a command</summary>
		this.name = command.name;

		var scriptSource = (function (scripts) {
			var scripts = document.getElementsByTagName('script'),
				script = scripts[scripts.length - 1];

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

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
            var parameters = {
                commandDescriptor: commandDescriptor
            };

			var url = self.origin + "/Bifrost/CommandCoordinator/Handle?_cmd=" + command._generatedFrom;

            server.post(url, parameters).continueWith(function (result) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(result);

                if (commandResult.success === true) {
                    systemEvents.commands.succeeded.trigger(result);
                } else {
                    systemEvents.commands.failed.trigger(result);
                }

                promise.signal(commandResult);
            }).onFail(function (response) {
                var commandResult = Bifrost.commands.CommandResult.create();
                commandResult.exception = "HTTP 500";
                commandResult.exceptionMessage = response.statusText;
                commandResult.details = response.responseText;
                systemEvents.commands.failed.trigger(commandResult);
                promise.signal(commandResult);
            });

            return promise;
        };
    })
});