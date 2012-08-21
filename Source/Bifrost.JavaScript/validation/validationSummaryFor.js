if (typeof ko !== 'undefined') {
    Bifrost.namespace("Bifrost.validation", {
        ValidationSummary: function (commands) {
            var self = this;
            this.commands = commands;
            this.messages = ko.computed(function () {
                var actualMessages = [];
                $.each(self.commands, function (commandIndex, command) {
                    $.each(command.validators, function (validatorIndex, validator) {
                        if (!validator.isValid()) {
                            actualMessages.push(validator.message());
                        }
                    });
                });
                return actualMessages;
            });
        }
    });

    ko.bindingHandlers.validationSummaryFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var target = ko.utils.unwrapObservable(value);
            if (!(target instanceof Array)) {
                target = [target];
            }

            var validationSummary = new Bifrost.validation.ValidationSummary(target);
            ko.applyBindingsToNode(element, { foreach: validationSummary.messages }, validationSummary);
        }
    };
}