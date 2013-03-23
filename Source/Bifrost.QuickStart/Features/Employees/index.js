Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    index: Bifrost.views.ViewModel.extend(function () {
        var self = this;

        this.value = ko.observable(42);
    })
});