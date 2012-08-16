Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;
        //recursively create JSON from mix of objects and knockout observables/computed values
        var commandContent = ko.toJS(commandParameters);
        commandContent.Id = id;
        this.Command = ko.toJSON(commandContent);
    };

    return {
        createFrom: function (command) {
            var commandDescriptor = new CommandDescriptor(command.name, command.id, command.parameters);
            return commandDescriptor;
        }
    };
})();
