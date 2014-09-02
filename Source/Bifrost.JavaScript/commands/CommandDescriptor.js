Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = function(command) {
    var self = this;

    var builtInCommand = {};
    if (typeof Bifrost.commands.Command !== "undefined") {
        builtInCommand = Bifrost.commands.Command.create({
            region: { commands: [] },
            commandCoordinator: {},
            commandValidationService: {},
            commandSecurityService: { getContextFor: function () { return { continueWith: function () { } }; } },
            mapper: {},
            options: {}
        });
    }

    function shouldSkipProperty(target, property) {
        if (!target.hasOwnProperty(property)) {
            return true;
        }
        if (builtInCommand.hasOwnProperty(property)) {
            return true;
        }
        if (ko.isObservable(target[property])) {
            return false;
        }
        if (typeof target[property] === "function") {
            return true;
        }
        if (property === "_type") {
            return true;
        }
        if (property === "_namespace") {
            return true;
        }

        return false;
    }

    function getPropertiesFromCommand(command) {
        var properties = {};

        for (var property in command) {
            if (!shouldSkipProperty(command, property) ) {
                properties[property] = command[property];
            }
        }
        return properties;
    }

    this.name = command._name;
    this.generatedFrom = command._generatedFrom;
    this.id = Bifrost.Guid.create();

    var properties = getPropertiesFromCommand(command);
    var commandContent = ko.toJS(properties);
    commandContent.Id = Bifrost.Guid.create();
    this.command = ko.toJSON(commandContent);
};


Bifrost.commands.CommandDescriptor.createFrom = function (command) {
    var commandDescriptor = new Bifrost.commands.CommandDescriptor(command);
    return commandDescriptor;
};

