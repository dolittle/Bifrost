Bifrost.namespace("Bifrost.commands", {
    commandSecurityService: Bifrost.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityContextFactory = commandSecurityContextFactory;

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();

            var context = self.commandSecurityContextFactory.create();
            
            var url = "/Bifrost/CommandSecurity/GetForCommand?commandName=" + command.name;
            $.getJSON(url, function (e) {
                context.isAuthorized(e.isAuthorized);
                promise.signal(context);
            });

            return promise;
        };
    })
});