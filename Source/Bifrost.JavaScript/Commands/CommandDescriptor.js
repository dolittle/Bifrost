Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;
        this.Command = ko.toJSON(commandParameters);
    };

    return {
        createFrom: function (command) {
            var commandDescriptor = new CommandDescriptor(command.name, command.id, command.parameters);
            return commandDescriptor;
        }
    }
})();
