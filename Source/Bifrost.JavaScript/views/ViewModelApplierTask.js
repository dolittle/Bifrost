Bifrost.namespace("Bifrost.views", {
    ViewModelApplierTask: Bifrost.views.ComposeTask.extend(function (view, masterViewModel, viewModelLoader, documentService, regionManager, viewModelManager) {
        /// <summary>Represents a task for applying a single viewModel</summary>
        var self = this;

        function applyViewModelsByAttribute(path, container, region, promise) {
            var viewModelApplied = false;

            var elements = documentService.getAllElementsWithViewModelFilesFrom(container);
            if (elements.length > 0) {

                function loadAndApply(target) {
                    viewModelApplied = true;
                    var viewModelFile = $(target).data("viewmodel-file");
                    viewModelLoader.load(viewModelFile, region).continueWith(function (instance) {
                        masterViewModel.applyBindingForViewModel(view.element, instance);
                        instance.region.viewModel = instance;
                        promise.signal(instance);
                    });
                }

                if (elements.length == 1) {
                    loadAndApply(elements[0]);
                } else {
                    for (var elementIndex = elements.length - 1; elementIndex > 0; elementIndex--) {
                        loadAndApply(elements[elementIndex]);
                    }
                }
            }

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            var promise = Bifrost.execution.Promise.create();
            if (viewModelManager.hasForView(path)) {
                var viewModelFile = Bifrost.Path.changeExtension(path, "js");
                documentService.setViewModelFileOn(container, viewModelFile);

                viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    masterViewModel.applyBindingForViewModel(target, instance);
                    instance.region.viewModel = instance;
                    promise.signal(instance);
                });
            } else {
                promise.signal(null);
            }
            
            return promise;
        }



        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var viewModelApplied = false;

            regionManager.getFor(view).continueWith(function (region) {
                if (viewModelManager.hasForView(view.path)) {
                    var viewModelFile = Bifrost.Path.changeExtension(view.path, "js");
                    documentService.setViewModelFileOn(view.element, viewModelFile);
                    
                    viewModelLoader.load(viewModelFile, region).continueWith(function (instance) {
                        masterViewModel.applyBindingForViewModel(view.element, instance);
                        
                        region.viewModel = instance;
                        promise.signal(instance);
                    });
                } else {
                    viewModelApplied = applyViewModelsByAttribute(view.path, view.element, region, promise);
                    if (viewModelApplied == false) {
                        applyViewModelByConventionFromPath(view.path, view.element, region).continueWith(function (instance) {
                            promise.signal(instance);
                        });
                    } else if( Bifrost.isNullOrUndefined(viewModelApplied) ) {
                        promise.signal(viewModelApplied);
                    }
                }
            });

            return promise;
        };
    })
});