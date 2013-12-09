Bifrost.namespace("Bifrost.views", {
    ViewLoadTask: Bifrost.views.ComposeTask.extend(function (files) {
        /// <summary>Represents a task for loading files asynchronously</summary>

        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var filesToLoad = [];

            files.forEach(function (file) {
                var path = file.path.fullPath;
                if (file.fileType === Bifrost.io.fileType.html) {
                    path = "text!" + path + "!strip";
                    if (!file.path.hasExtension()) {
                        path = "noext!" + path;
                    } 
                }

                filesToLoad.push(path);
            });

            require(filesToLoad, function (view) {
                promise.signal(view);
            });
            return promise;
        }
    })
});