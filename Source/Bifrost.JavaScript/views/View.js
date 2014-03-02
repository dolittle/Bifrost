Bifrost.namespace("Bifrost.views", {
    View: Bifrost.Type.extend(function (viewLoader, viewModelTypes, viewModelManager, path) {
        var self = this;

        this.path = path;
        this.content = "[CONTENT NOT LOADED]";
        this.element = null;
        this.viewModelType = null;
        this.viewModelPath = null;

        this.load = function (region) {
            var promise = Bifrost.execution.Promise.create();
            self.viewModelPath = viewModelManager.getViewModelPathForView(path);
            viewLoader.load(self.path, region).continueWith(function (html) {
                self.content = html;
                self.viewModelType = viewModelTypes.getViewModelTypeForPath(self.viewModelPath);
                promise.signal(self);
            });

            return promise;
        };
    })
});