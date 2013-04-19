Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    index: Bifrost.Type.extend(function () {
        var self = this;
    })
});
Bifrost.features.featureManager.get("Employees/index").defineViewModel(Bifrost.QuickStart.Features.Employees.index);