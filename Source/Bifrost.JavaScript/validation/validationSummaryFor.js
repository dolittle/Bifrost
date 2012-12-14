if (typeof ko !== 'undefined') {
    Bifrost.namespace("Bifrost.validation", {
        ValidationSummary: function (commands) {
            var self = this;
            this.commands = ko.observable(commands);
            this.messages = ko.computed(function () {
                var actualMessages = [];
                $.each(self.commands(), function (commandIndex, command) {
                    var unwrappedCommand = ko.utils.unwrapObservable(command);
                    
                    $.each(unwrappedCommand.validators, function (validatorIndex, validator) {
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
            var target = ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor);
            var validationSummary = new Bifrost.validation.ValidationSummary(target);
            ko.utils.domData.set(element, 'validationsummary', validationSummary);
            ko.applyBindingsToNode(element, { foreach: validationSummary.messages }, validationSummary);
        },
        update: function (element, valueAccessor) {
            var validationSummary = ko.utils.domData.get(element, 'validationsummary');
            validationSummary.commands = ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor);
        },
        getValueAsArray: function (valueAccessor) {
            var target = ko.utils.unwrapObservable(valueAccessor());
            if (!(target instanceof Array)) { target = [target]; }
            return target;
        }
    };
}