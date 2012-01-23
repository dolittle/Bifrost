var Bifrost = Bifrost || {};

if (typeof ko === 'undefined') {
    throw "Requires Knockout.js";
}

ko.bindingHandlers.command = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        ko.applyBindingsToNode(element, { click: valueAccessor().execute }, viewModel);
    }
};

(function () {
    function CommandDescriptor(name, commandParameters) {
        this.Name = name;

        var commandContent = {
            Id: guid()
        };

        for (var parameter in commandParameters) {
            if (typeof (commandParameters[parameter]) == "function") {
                commandContent[parameter] = commandParameters[parameter]();
            } else {
                commandContent[parameter] = commandParameters[parameter];
            }
        }


        this.Command = JSON.stringify(commandContent);
    };

    function Command() {
        var self = this;
        this.hasError = false;
        this.isBusy = ko.observable();
        this.canExecute = ko.observable(true);

        this.execute = function () {
            self.hasError = false;
            if (self.beforeExecute) {
                self.beforeExecute(self);
            }

            if (!self.canExecute()) {
                return;
            }
            self.isBusy(true);

            $.ajax({
                url: "/CommandCoordinator/Handle",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify({ commandDescriptor : JSON.stringify(new CommandDescriptor(self.name, self.parameters)) }),
                contentType: 'application/json; charset=utf-8',
                error: function (e) {
                    self.hasError = true;
                    self.error = e;

                    if (self.onError != undefined) {
                        self.onError(self);
                    }
                },
                complete: function () {
                    if (!self.hasError) {
                        if (self.onSuccess != undefined) {
                            self.onSuccess(self);
                        }
                    }
                    if (self.onComplete != undefined) {
                        self.onComplete(self);
                    }
                    self.isBusy(false);
                }
            });
        };
    };

    Bifrost.commands = {
        create: function (configuration) {
            var cmd = new Command();
            $.extend(cmd, configuration);
            return cmd;
        }
    };
})();
