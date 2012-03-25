Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandResult = (function () {
    function CommandResult(existing) {
        var self = this;
        this.isEmpty = function () {
            return self.commandId === Bifrost.Guid.empty;
        };

        if (typeof existing !== "undefined") {
            Bifrost.extend(this, existing);
        } else {
            this.commandName = "";
            this.commandId = Bifrost.Guid.empty;
            this.validationResult = [];
            this.success = true;
            this.invalid = false;
            this.exception = undefined;
        }
    }

    return {
        create: function() {
            var commandResult = new CommandResult();
            return commandResult;
        },
        createFrom: function (result) {
            var existing = typeof result === "string" ? $.parseJSON(result) : result;
            var commandResult = new CommandResult(existing);
            return commandResult;
        }
    };
})();