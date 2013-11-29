Bifrost.namespace("Bifrost.views", {
    ViewLoadTask: Bifrost.tasks.LoadTask.extend(function(files) {
        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            setTimeout(function () {
                require(files, function (view) {
                    promise.signal(view);
                });
            }, 1000);
            return promise;
        }
    })
});