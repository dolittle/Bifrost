Bifrost.namespace("Bifrost.commands");
Bifrost.commands.commandCoordinator = (function () {
    var baseUrl = "/CommandCoordinator";
    function sendToHandler(url, data, options) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8',
            error: function (e) {
                if (typeof options.error === "function") {
                    options.error(e);
                }
            },
            complete: function (e) {
                if (typeof options.complete === "function") {
                    options.complete(e);
                }
            }
        });
    }

    return {
        handle: function(command, options) {
            var methodParameters = {
                commandDescriptor: JSON.stringify(Bifrost.commands.CommandDescriptor.createFrom(command))
            };

            sendToHandler(baseUrl + "/Handle", JSON.stringify(methodParameters), options);
        },
        handleForSaga: function(saga, commands, options) {
            var commandDescriptors = [];
            $.each(commands, function(index, command) {
                command.onBeforeExecute();
                commandDescriptors.push(Bifrost.commands.CommandDescriptor.createFrom(command));
            });

            var methodParameters = {
                sagaId: "\"" + saga.id + "\"",
                commandDescriptors: JSON.stringify(commandDescriptors)
            };

            sendToHandler(baseUrl + "/HandleForSaga", JSON.stringify(methodParameters), options);

        }
    };
})();
