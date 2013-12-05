Bifrost.namespace("Bifrost.commands", {
    commandCoordinator: Bifrost.Singleton(function (taskFactory) {
        this.handle = function (command) {
            var promise = Bifrost.execution.Promise.create();
            var task = taskFactory.createHandleCommand(command);

            command.region.tasks.execute(task).continueWith(function (commandResult) {
                promise.signal(commandResult);
            });

            return promise;
        };

        this.handleMany = function (commands, region) {
            var promise = Bifrost.execution.Promise.create();

            try {
                var task = taskFactory.createHandleCommands(commands);

                region.tasks.execute(task).continueWith(function (commandResults) {
                    commands.forEach(function (command, index) {
                        var commandResult = commandResults[index];
                        if (commandResult != null && !Bifrost.isUndefined(commandResult)) {
                            command.handleCommandResult(commandResult);
                        }
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
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandCoordinator = Bifrost.commands.commandCoordinator;