Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    test: Bifrost.views.ViewModel.extend(function () {
        var self = this;

        this.identifier = Bifrost.Guid.create();
        console.log("\n\n*** Test viewModel created ***\n\n");
    })
});
