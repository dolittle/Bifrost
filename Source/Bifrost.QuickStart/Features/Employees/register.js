Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    register: Bifrost.views.ViewModel.extend(function (registerEmployee, globalMessenger) {
        var self = this;

        this.globalMessenger = globalMessenger;

        this.register = registerEmployee;

        this.register.complete(function () {
            setTimeout(function () {
                self.globalMessenger.publish("employeeRegistered");
            }, 100);
        });
    })
});
