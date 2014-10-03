Bifrost.namespace("Web.HumanResources.Employees", {
    test: Bifrost.views.ViewModel.extend(function () {
        var self = this;

        this.identifier = Bifrost.Guid.create();
    })
});
