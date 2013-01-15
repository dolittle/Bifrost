Bifrost.namespace("Bifrost.commands", {
    commandCoordinator: Bifrost.Singleton(function () {
        var baseUrl = "/CommandCoordinator";
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
            var methodParameters = {
                commandDescriptor: JSON.stringify(Bifrost.commands.CommandDescriptor.createFrom(command))
            };

            sendToHandler(baseUrl + "/Handle", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                promise.signal(commandResult);
            });

            return promise;
        };
        this.handleForSaga = function (saga, commands, options) {
            throw "not implemented";
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandCoordinator = Bifrost.commands.commandCoordinator;