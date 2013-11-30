Bifrost.namespace("Bifrost.tasks", {
    FileLoadTask: Bifrost.tasks.LoadTask.extend(function (files) {
        /// <summary>Represents a task for loading view related files asynchronously</summary>

        this.files = files;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            require(files, function () {
                promise.signal();
            });
            return promise;
        }
    })
});