Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    list: Bifrost.views.ViewModel.extend(function (allEmployees, globalMessenger) {
        var self = this;
        this.employees = allEmployees.paged(4,1);

        globalMessenger.subscribeTo("employeeRegistered", function () {
            allEmployees.execute();
        });
    })
});