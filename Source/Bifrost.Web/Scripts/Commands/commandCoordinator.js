Bifrost.namespace("Bifrost.commands");
Bifrost.commands.commandCoordinator = (function () {
    var baseUrl = "/CommandCoordinator";
    return {
        handle: function (command, options) {
            var methodParameters = {
                commandDescriptor: JSON.stringify(Bifrost.commands.CommandDescriptor.create(command))
            };

            $.ajax({
                url: baseUrl + "/Handle",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(methodParameters),
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
        },
        handleForSaga: function (saga, commands, options) {
            var commandDescriptors = [];
            $.each(commands, function (index, command) {
                command.onBeforeExecute();
                commandDescriptors.push(Bifrost.commands.CommandDescriptor.createFrom(command));
            });

            var methodParameters = {
                sagaId: "\"" + saga.id + "\"",
                commandDescriptors: JSON.stringify(commandDescriptors)
            }

            $.ajax({
                url: baseUrl + "/HandleForSaga",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(methodParameters),
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
    }
})();
