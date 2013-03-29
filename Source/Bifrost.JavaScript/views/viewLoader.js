Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager) {
        var self = this;

        this.viewModelManager = viewModelManager;
        
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();

            if (!path.startsWith("/")) path = "/" + path;

            var files = [];
            files.push("text!" + path + "!strip");

            if (self.viewModelManager.hasForView(path)) {
                var viewModelFile = Bifrost.path.changeExtension(path, "js");
                files.push(viewModelFile);
            }

            require(files, function (view) {
                promise.signal(view);
            });
            return promise;
        };
    })
});