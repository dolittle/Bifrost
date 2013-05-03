Bifrost.namespace("Bifrost.commands", {
    commandSecurityContextFactory: Bifrost.Singleton(function () {
        var self = this;

        this.create = function () {
            var context = Bifrost.commands.CommandSecurityContext.create();
            return context;
        };
    })
});