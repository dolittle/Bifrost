Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    popup: Bifrost.views.ViewModel.extend(function () {
        var self = this;

        this.identifier = Bifrost.Guid.create();
        console.log("\n\n*** Popup viewModel created ***\n\n");
    })
});
