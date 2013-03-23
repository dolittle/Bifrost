Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager) {
        var self = this;
        this.assetsManager = assetsManager;

        this.get = function (path) {
            var promise = Bifrost.execution.Promise.create();
            require([path], function () {
                var localPath = Bifrost.path.getPathWithoutFilename(path);
                var filename = Bifrost.path.getFilenameWithoutExtension(path);

                for (var mapperKey in Bifrost.namespaceMappers) {
                    var mapper = Bifrost.namespaceMappers[mapperKey];
                    if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                        var namespacePath = mapper.resolve(localPath);
                        var namespace = Bifrost.namespace(namespacePath);

                        if (filename in namespace) {
                            var instance = namespace[filename].create();
                            promise.signal(instance);
                        }
                    }
                }
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