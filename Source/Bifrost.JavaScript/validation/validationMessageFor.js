if (typeof ko !== 'undefined') {
    ko.bindingHandlers.validationMessageFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var validator = value.validator;
            ko.applyBindingsToNode(element, { hidden: validator.isValid, text: validator.message }, validator);
        }
    };
}