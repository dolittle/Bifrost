Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager) {
        var self = this;

        this.viewModelManager = viewModelManager;
        
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();

            if (!path.startsWith("/")) path = "/" + path;

            var files = [];

            var viewFile = "text!" + path + "!strip";
            if (!Bifrost.path.hasExtension(viewFile)) viewFile = "noext!" + viewFile;
            files.push(viewFile);

            if (self.viewModelManager.hasForView(path)) {
                var viewModelFile = self.viewModelManager.getViewModelPathForView(path);
                files.push(viewModelFile);
            }

            require(files, function (view) {
                promise.signal(view);
            });
            return promise;
        };
    })
});