Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    list: Bifrost.views.ViewModel.extend(function (allEmployees) {
        var self = this;
        this.employees = allEmployees.paged(8,0);

        self.region.globalMessenger.subscribeTo("employeeRegistered", function () {
            self.employees.execute();
        });
    })
});