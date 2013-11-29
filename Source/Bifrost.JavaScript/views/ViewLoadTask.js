Bifrost.namespace("Bifrost.views", {
    ViewLoadTask: Bifrost.tasks.LoadTask.extend(function(files) {
        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            require(files, function (view) {
                promise.signal(view);
            });
            return promise;
        }
    })
});