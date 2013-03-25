Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager) {
        var self = this;
        this.assetsManager = assetsManager;

        var partialViewModelBindingProvider = function () {
            var self = this;

            var originalBindingProvider = ko.bindingProvider.instance;

            this.nodeHasBindings = function (node) {
                var closestViewModel = $(node).closest("[data-viewmodel]");
                if (closestViewModel.length == 1) {
                    var viewModelName = closestViewModel.data("viewmodel");
                    if (viewModelName == self.currentViewModel) {
                        return originalBindingProvider.nodeHasBindings(node);
                    } else {
                        return false;
                    }
                }

                return originalBindingProvider.nodeHasBindings(node)
            },

            this.getBindings = function (node, bindingContext) {
                return originalBindingProvider.getBindings(node, bindingContext);
            }
        }

        function applyViewModel(instance, target, viewModelFile) {
            $(target).data("viewmodel",instance);
            var previousBindingProvider = ko.bindingProvider.instance;
            ko.bindingProvider.instance = new partialViewModelBindingProvider();
            ko.bindingProvider.instance.currentViewModel = viewModelFile;
            ko.applyBindings(instance, target);
            ko.bindingProvider.instance.currentViewModel = "";
            ko.bindingProvider.instance = previousBindingProvider;
        }



        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            $("[data-viewmodel]", container).each(function () {
                viewModelApplied = true;
                var target = $(this)[0];
                var viewModelName = $(this).attr("data-viewmodel");
                self.get(viewModelName, path).continueWith(function (instance) {
                    
                    applyViewModel(instance, target, viewModelName);
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                self.getForView(path).continueWith(function (instance) {
                    applyViewModel(instance, container, path);
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