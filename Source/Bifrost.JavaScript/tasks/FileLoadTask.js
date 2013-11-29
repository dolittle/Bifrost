Bifrost.namespace("Bifrost.tasks", {
    FileLoadTask: Bifrost.tasks.LoadTask.extend(function(files) {
        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            require(files, function () {
                promise.signal();
            });
            return promise;
        }
    })
});