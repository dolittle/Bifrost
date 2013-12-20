Bifrost.namespace("Bifrost.commands", {
    commandSecurityService: Bifrost.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityContextFactory = commandSecurityContextFactory;

        function getTypeNameFor(command) {
            return command._type._name;
        }

        function getSecurityContextNameFor(type) {
            var securityContextName = type + "SecurityContext";
            return securityContextName;
        }


        function hasSecurityContextInNamespaceFor(type, namespace) {
            var securityContextName = getSecurityContextNameFor(type);
            return namespace.hasOwnProperty(securityContextName);
        }

        function getSecurityContextInNamespaceFor(type, namespace) {
            var securityContextName = getSecurityContextNameFor(type, namespace);
            return namespace[securityContextName];
        }

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();

            var type = getTypeNameFor(command);
            if (hasSecurityContextInNamespaceFor(type, command._type._namespace)) {
                var contextType = getSecurityContextInNamespaceFor(type, command._type._namespace);
                var context = contextType.create();
                promise.signal(context);
            } else {
                var context = self.commandSecurityContextFactory.create();
                if (typeof command.generatedFrom == "undefined" || command.generatedFrom == "") {
                    promise.signal(context);
                } else {
                    var url = "/Bifrost/CommandSecurity/GetForCommand?commandName=" + command.generatedFrom;
                    $.getJSON(url, function (e) {
                        context.isAuthorized(e.isAuthorized);
                        promise.signal(context);
                    });
                }
            }

            return promise;
        };

        this.getContextForType = function (commandType) {
            var command = commandType.create({ region: { commands: [] } });
            var context = self.getContextFor(command);
            return context;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandSecurityService = Bifrost.commands.commandSecurityService;