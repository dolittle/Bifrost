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
            this.CommandName = "";
            this.CommandId = Bifrost.Guid.empty;
            this.ValidationResult = [];
            this.Success = true;
            this.Invalid = false;
            this.Exception = undefined;
        }
    }

    return {
        create: function () {
            var commandResult = new CommandResult();
            return commandResult;
        },
        createFrom: function (json) {
            var existing = $.parseJSON(json);
            var commandResult = new CommandResult(existing);
            return commandResult;
        }
    }
})();