Bifrost.namespace("web.features.employees", {
    register: Bifrost.Type.extend(function (registerEmployee, allEmployees) {
        var self = this;

        this.register = registerEmployee;
        this.employees = allEmployees.all();
    })
});
Bifrost.features.featureManager.get("Employees/register").defineViewModel(web.features.employees.register);