Bifrost.namespace("Bifrost.commands", {
    commandSecurityContextFactory: Bifrost.Singleton(function () {
        this.create = function () {
            var context = Bifrost.commands.CommandSecurityContext.create();
            return context;
        };
    })
});