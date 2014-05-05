Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    list: Bifrost.views.ViewModel.extend(function (allEmployees) {
        var self = this;
        this.employees = allEmployees.all();

        self.region.globalMessenger.subscribeTo("employeeRegistered", function () {
            self.employees.execute();
        });
    })
});