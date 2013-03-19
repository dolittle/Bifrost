Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager) {
        var self = this;
        this.assetsManager = assetsManager;

        this.get = function (path) {
            var promise = Bifrost.execution.Promise.create();
            require([path], function () {
                var i = 0;
                i++;
            });
            return promise;
        };

        this.hasForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            scriptFile = Bifrost.path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return self.get(scriptFile);
        };
    })
});