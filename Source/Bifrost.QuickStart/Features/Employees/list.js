Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    list: Bifrost.Type.extend(function (allEmployees) {
        var self = this;
        this.employees = allEmployees.all();
    })
});
Bifrost.features.featureManager.get("Employees/list").defineViewModel(Bifrost.QuickStart.Features.Employees.list);