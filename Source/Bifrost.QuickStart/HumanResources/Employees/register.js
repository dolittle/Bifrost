Bifrost.namespace("Web.HumanResources.Employees", {
    register: Bifrost.views.ViewModel.extend(function (registerEmployee) {
        var self = this;

        this.register = registerEmployee;

        this.register.succeeded(function () {
            setTimeout(function () {
                self.region.globalMessenger.publish("employeeRegistered");
            }, 100);
        });
    })
});