Bifrost.namespace("Bifrost.commands", {
    commandSecurityService: Bifrost.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityService = commandSecurityContextFactory;

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();

            var context = self.commandSecurityService.create();

            promise.signal(context);

            return promise;
        };
    })
});