Bifrost.namespace("Bifrost.views", {
    ViewLoadTask: Bifrost.tasks.LoadTask.extend(function (files) {
        /// <summary>Represents a task for loading files asynchronously</summary>

        this.files = files;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            require(files, function (view) {
                promise.signal(view);
            });
            return promise;
        }
    })
});