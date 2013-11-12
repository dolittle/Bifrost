Bifrost.namespace("Bifrost.commands", {
    commandSecurityService: Bifrost.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityContextFactory = commandSecurityContextFactory;

        function getSecurityContextNameFor(command) {
            var securityContextName = command._type._name + "SecurityContext";
            return securityContextName;
        }


        function hasSecurityContextInNamespaceFor(command) {
            var securityContextName = getSecurityContextNameFor(command);
            return command._type._namespace.hasOwnProperty(securityContextName);
        }

        function getSecurityContextInNamespaceFor(command) {
            var securityContextName = getSecurityContextNameFor(command);
            return command._type._namespace[securityContextName];
        }

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();

            if( hasSecurityContextInNamespaceFor(command) ) {
                var context = getSecurityContextInNamespaceFor(command);
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
    })
});