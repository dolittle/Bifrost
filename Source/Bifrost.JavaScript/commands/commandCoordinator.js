Bifrost.namespace("Bifrost.commands", {
    commandCoordinator: Bifrost.Singleton(function () {
        var baseUrl = "/Bifrost/CommandCoordinator";
        function sendToHandler(url, data, completeHandler) {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: data,
                contentType: 'application/json; charset=utf-8',
                complete: completeHandler
            });
        }

        function handleCommandCompletion(jqXHR, command, commandResult) {
            if (jqXHR.status === 200) {
                command.result = Bifrost.commands.CommandResult.createFrom(commandResult);
                command.hasExecuted = true;
                if (command.result.success === true) {
                    command.onSuccess();
                } else {
                    command.onError();
                }
            } else {
                command.result.success = false;
                command.result.exception = {
                    Message: jqXHR.responseText,
                    details: jqXHR
                };
                command.onError();
            }
            command.onComplete();
        }


        this.handle = function (command) {
            var promise = Bifrost.execution.Promise.create();
            var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
            var methodParameters = {
                commandDescriptor: JSON.stringify(commandDescriptor)
            };

            sendToHandler(baseUrl + "/Handle?_cmd=" + command.generatedFrom, JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                promise.signal(commandResult);
            });

            return promise;
        };

        this.handleMany = function (commands) {
            var promise = Bifrost.execution.Promise.create();
            var commandDescriptors = [];

            commands.forEach(function (command) {
                command.isBusy(true);
                var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
                commandDescriptors.push(commandDescriptor);
            });

            try {
                var methodParameters = {
                    commandDescriptors: JSON.stringify(commandDescriptors)
                };

                sendToHandler(baseUrl + "/HandleMany", JSON.stringify(methodParameters), function (jqXHR) {
                    var results = JSON.parse(jqXHR.responseText);

                    var commandResults = [];

                    results.forEach(function (result) {
                        var commandResult = Bifrost.commands.CommandResult.createFrom(result);
                        commandResults.push(commandResult);
                    });

                    commands.forEach(function(command, index) {
                        command.handleCommandResult(commandResults[index]);
                        command.isBusy(false);
                    });

                    promise.signal(commandResults);
                });
            } catch(e) {
                commands.forEach(function(command) {
                    command.isBusy(false);
                });
            }

            return promise;
        };

        this.handleForSaga = function (saga, commands, options) {
            throw "not implemented";
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandCoordinator = Bifrost.commands.commandCoordinator;