Bifrost.namespace("Bifrost.views", {
    ViewModelLoadTask: Bifrost.views.ComposeTask.extend(function (files) {
        /// <summary>Represents a task for loading viewModels</summary>
        this.files = files;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            require(files, function () {
                promise.signal();
            });
            return promise;
        };
    })
});