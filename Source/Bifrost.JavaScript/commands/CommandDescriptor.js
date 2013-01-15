Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;
        //recursively create JSON from mix of objects and knockout observables/computed values
        var commandContent = ko.toJS(commandParameters);
        commandContent.Id = Bifrost.Guid.create();
        this.Command = ko.toJSON(commandContent);
    };

    return {
        createFrom: function (command) {
            var properties = {};

            for (var property in command) {
                if (command.hasOwnProperty(property) && ko.isObservable(command[property])) {
                    properties[property] = command[property];
                }
            }

            var commandDescriptor = new CommandDescriptor(command.name, command.id, properties);
            return commandDescriptor;
        }
    };
})();
