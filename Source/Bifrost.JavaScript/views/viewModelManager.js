Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager) {
        var self = this;
        this.assetsManager = assetsManager;

        var partialViewModelBindingProvider = function () {
            var self = this;

            var originalBindingProvider = ko.bindingProvider.instance;

            this.nodeHasBindings = function (node) {
                if (typeof $(node).data("navigation-frame") !== "undefined") {
                    return false;
                }

                var closestViewModel = $(node).closest("[data-viewmodel-file]");
                if (closestViewModel.length == 1) {
                    var viewModelName = closestViewModel.data("viewmodel-file");
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

        function applyViewModel(instance, target) {
            var viewModelFile = $(target).data("viewmodel-file");

            target.viewModel = instance;

            $(target).find("*").each(function () {
                $(this).unbind();
            });
            ko.cleanNode(target);

            var previousBindingProvider = ko.bindingProvider.instance;
            ko.bindingProvider.instance = new partialViewModelBindingProvider();
            ko.bindingProvider.instance.currentViewModel = viewModelFile;
            ko.applyBindings(instance, target);
            ko.bindingProvider.instance.currentViewModel = "";
            ko.bindingProvider.instance = previousBindingProvider;

            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }



        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            $("[data-viewmodel-file]", container).each(function () {
                viewModelApplied = true;
                var target = $(this)[0];
                var viewModelFile = $(this).attr("data-viewmodel-file");
                self.get(viewModelFile, path).continueWith(function (instance) {
                    applyViewModel(instance, target, viewModelFile);
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                var viewModelFile = Bifrost.path.changeExtension(path, "js");
                $(container).data("viewmodel-file", viewModelFile);
                self.getForView(path).continueWith(function (instance) {
                    applyViewModel(instance, container);
                });
            }
        }

        function applyViewModelInMemory(path, callback) {
            var localPath = Bifrost.path.getPathWithoutFilename(path);
            var filename = Bifrost.path.getFilenameWithoutExtension(path);
            var wasInMemory = false;

            for (var mapperKey in Bifrost.namespaceMappers) {
                var mapper = Bifrost.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(localPath);
                    var namespace = Bifrost.namespace(namespacePath);

                    if (filename in namespace) {
                        wasInMemory = true;
                        namespace[filename].beginCreate().continueWith(function (instance) {
                            callback(instance);
                        });
                    }
                }
            }

            return wasInMemory;
        }


        this.get = function (path) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;
            require([path], function () {
                applyViewModelInMemory(path, function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.hasForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            scriptFile = Bifrost.path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getViewModelPathForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return scriptFile;
        };

        this.getForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return self.get(scriptFile);
        };

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = false;

            viewModelApplied = applyViewModelInMemory(view.path, function (instance) {
                $(view.element).data("viewmodel-file", Bifrost.path.changeExtension(view.path, "js"));
                applyViewModel(instance, view.element);
            });
            if (viewModelApplied == false) {
                viewModelApplied = applyViewModelsByAttribute(view.path, view.element);
                if (viewModelApplied == false) {
                    applyViewModelByConventionFromPath(view.path, view.element);
                }
            }
        };
    })
});