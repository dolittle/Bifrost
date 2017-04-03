Bifrost.namespace("Web", {
    index: Bifrost.views.ViewModel.extend(function (myCommand) {
        myCommand.execute();
        console.log("Hello from VM");

    })
});