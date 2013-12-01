Bifrost.namespace("Bifrost.interaction", {
    CommandOperation: Bifrost.interaction.Operation.extend(function () {
        /// <summary>Represents an operation that result in a command</summary>
        var self = this;

        this.createCommandOfType = function (commandType) {
            /// <summary>Create an instance of a given command type</summary>
            commandType.create({
                region: self.region
            });
        };
    })
});