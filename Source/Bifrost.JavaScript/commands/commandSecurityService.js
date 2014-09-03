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
            return Bifrost.isNullOrUndefined(securityContextName) &&
                !Bifrost.isNullOrUndefined(namespace) &&
                namespace.hasOwnProperty(securityContextName);
        }

        function getSecurityContextInNamespaceFor(type, namespace) {
            var securityContextName = getSecurityContextNameFor(type, namespace);
            return namespace[securityContextName];
        }

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();
            var context;

            var type = getTypeNameFor(command);
            if (hasSecurityContextInNamespaceFor(type, command._type._namespace)) {
                var contextType = getSecurityContextInNamespaceFor(type, command._type._namespace);
                context = contextType.create();
                promise.signal(context);
            } else {
                context = self.commandSecurityContextFactory.create();
                if (Bifrost.isNullOrUndefined(command._generatedFrom) || command._generatedFrom === "") {
                    promise.signal(context);
                } else {
                    var url = "/Bifrost/CommandSecurity/GetForCommand?commandName=" + command._generatedFrom;
                    $.getJSON(url, function (e) {
                        context.isAuthorized(e.isAuthorized);
                        promise.signal(context);
                    });
                }
            }

            return promise;
        };

        this.getContextForType = function (commandType) {
            var promise = Bifrost.execution.Promise.create();
            var context;

            if (hasSecurityContextInNamespaceFor(commandType._name, commandType._namespace)) {
                var contextType = getSecurityContextInNamespaceFor(commandType._name, commandType._namespace);
                context = contextType.create();
                promise.signal(context);
            } else {
                context = Bifrost.commands.CommandSecurityContext.create();
                context.isAuthorized(true);
                promise.signal(context);
            }

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandSecurityService = Bifrost.commands.commandSecurityService;