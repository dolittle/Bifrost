Bifrost.namespace("Bifrost.commands");
Bifrost.commands.commandCoordinator = (function () {
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
            command.result = commandResult;
            command.hasExecuted = true;
            if (command.result.Success === true) {
                command.onSuccess();
            } else {
                command.onError();
            }
        } else {
            command.result.Success = false;
            command.result.Exception = {
                Message: jqXHR.responseText,
                details: jqXHR
            };
            command.onError();
        }
        command.onComplete();
    }

    return {
        handle: function (command) {
            var methodParameters = {
                commandDescriptor: JSON.stringify(Bifrost.commands.CommandDescriptor.createFrom(command))
            };

            sendToHandler(baseUrl + "/Handle", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                handleCommandCompletion(jqXHR, command, commandResult);
            });
        },
        handleForSaga: function (saga, commands) {
            var commandDescriptors = [];
            $.each(commands, function (index, command) {
                command.onBeforeExecute();
                commandDescriptors.push(Bifrost.commands.CommandDescriptor.createFrom(command));
            });

            var methodParameters = {
                sagaId: "\"" + saga.Id + "\"",
                commandDescriptors: JSON.stringify(commandDescriptors)
            };

            sendToHandler(baseUrl + "/HandleForSaga", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResultArray = $.parseJSON(jqXHR.responseText);

                $.each(commandResultArray, function (commandResultIndex, commandResult) {
                    $.each(commands, function (commandIndex, command) {
                        if (command.id === commandResult.CommandId) {
                            handleCommandCompletion(jqXHR, command, commandResult);
                            return false;
                        }
                    });
                });
            });
        }
    };
})();
