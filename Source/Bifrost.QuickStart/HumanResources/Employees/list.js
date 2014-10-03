Bifrost.namespace("Web.HumanResources.Employees", {
    list: Bifrost.views.ViewModel.extend(function (allEmployees, testHub) {
        var self = this;
        this.employees = allEmployees.paged(8, 0);


        testHub.server.getSomething("someString", 43).continueWith(function (result) {
            var i = 0;
            i++;
        });

        testHub.client(function (client) {
            client.doSomething = function (someString, someInt) {
                var i = 0;
                i++;
            };
        });

        self.region.globalMessenger.subscribeTo("employeeRegistered", function () {
            self.employees.execute();
        });
    })
});