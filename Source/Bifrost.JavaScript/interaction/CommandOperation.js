Bifrost.namespace("Bifrost.interaction", {
    CommandOperation: Bifrost.interaction.Operation.extend(function (commandSecurityService) {
        /// <summary>Represents an operation that result in a command</summary>
        var self = this;

        /// <field name="commandType" type="Bifrost.Type">Type of command to create</field>
        this.commandType = ko.observable();

        this.canPerform(false);

        this.commandType.subscribe(function (type) {
            commandSecurityService.getContextForType(type).continueWith(function (context) {
                if (!Bifrost.isNullOrUndefined(context)) self.canPerform(context.isAuthorized());
            });
        });

        this.createCommandOfType = function (commandType) {
            /// <summary>Create an instance of a given command type</summary>
            var instance = commandType.create({
                region: self.region
            });
            return instance;
        };
    })
});