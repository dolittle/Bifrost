Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    register: Bifrost.Type.extend(function (registerEmployee) {
        var self = this;
        this.register = registerEmployee;
    })
});
Bifrost.features.featureManager.get("Employees/register").defineViewModel(Bifrost.QuickStart.Features.Employees.register);