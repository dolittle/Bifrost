ko.bindingHandlers.tasks = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {

        ko.applyBindingsToNode(element, { foreach: valueAccessor() }, viewModel);

        return { controlsDescendantBindings: true };

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
    }
}

Bifrost.features.featureManager.get("Tasks").defineViewModel(function () {
    var self = this;



    this.tasks = ko.observable([
        {
            type: "ResetAllEventsForAllSubscriptionsTask",
            id: "asdasd",
            currentOperation: 0,
            state: {
                counter: 0
            }
        }
    ]);

});