Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;
        var commandContent = {
            Id: id
        };
        for (var property in commandParameters) {
            commandContent[property] = commandParameters[property];
        }
        this.Command = ko.toJSON(commandContent);
    };

    return {
        createFrom: function(command) {
            var commandDescriptor = new CommandDescriptor(command.name, command.id, command.parameters);
            return commandDescriptor;
        }
    };
})();
