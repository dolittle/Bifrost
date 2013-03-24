Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager) {
        var self = this;
        this.assetsManager = assetsManager;

        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            $("[data-viewmodel]", container).each(function () {
                viewModelApplied = true;
                var target = $(this)[0];
                var viewModelName = $(this).attr("data-viewmodel");
                self.get(viewModelName, path).continueWith(function (instance) {
                    ko.applyBindings(instance, target);
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                self.getForView(path).continueWith(function (instance) {
                    ko.applyBindings(instance, container);
                });
            }
        }

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

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = applyViewModelsByAttribute(view.path, view.element);
            if (viewModelApplied == false) {
                applyViewModelByConventionFromPath(view.path, view.element);
            }
        };
    })
});