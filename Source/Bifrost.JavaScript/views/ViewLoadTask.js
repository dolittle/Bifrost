Bifrost.namespace("Bifrost.views", {
    ViewLoadTask: Bifrost.views.ComposeTask.extend(function (files, fileManager) {
        /// <summary>Represents a task for loading files asynchronously</summary>

        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            fileManager.load(files).continueWith(function (instances) {
                var view = instances[0];
                promise.signal(view);
            });
            return promise;
        }
    })
});