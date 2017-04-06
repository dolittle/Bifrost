Bifrost.namespace("Web", {
    index: Bifrost.views.ViewModel.extend(function (myCommand, myQuery) {

        myCommand.something("Hello world");
        myCommand.execute();
        console.log("Hello from VM");

    })
});