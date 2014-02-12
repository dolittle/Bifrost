Bifrost.namespace("Bifrost.views", {
    ViewModelsApplierTask: Bifrost.views.ComposeTask.extend(function (root, viewModelLoader, viewModelManager, documentService, regionManager) {
        /// <summary>Represents a task for applying view models</summary>
        var self = this;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var elements = documentService.getAllElementsWithViewModelFilesFrom(root);
            var loadedViewModels = 0;

            elements.forEach(function (element) {
                var masterViewModel = viewModelManager.masterViewModel;
                var viewModelFile = documentService.getViewModelFileFrom(element);
                var viewFile = documentService.getViewFileFrom(element);
                

                var view = Bifrost.views.View.create({
                    viewLoader: {
                        load: function () {
                            var promise = Bifrost.execution.Promise.create();
                            promise.signal(element.innerHTML);
                            return promise;
                        }
                    },
                    path: viewFile
                });
                view.element = element;
                view.content = element.innerHTML;

                var viewModelParameters = documentService.getViewModelParametersFrom(element);
                regionManager.getFor(view).continueWith(function (region) {
                    viewModelLoader.load(viewModelFile, region, viewModelParameters).continueWith(function (instance) {
                        documentService.setViewModelOn(element, instance);

                        loadedViewModels++;

                        if (loadedViewModels == elements.length) {
                            elements.forEach(function (elementToApplyBindingsTo) {
                                var viewModel = documentService.getViewModelFrom(elementToApplyBindingsTo);
                                if (documentService.pageHasViewModel(masterViewModel)) {
                                    masterViewModel.applyBindingsForViewModel(elementToApplyBindingsTo, viewModel);
                                } else {
                                    masterViewModel.applyBindingExpressionForViewModel(elementToApplyBindingsTo, viewModel);
                                }
                            });

                            if (!documentService.pageHasViewModel(masterViewModel)) {
                                ko.applyBindings(masterViewModel);
                            } 
                            promise.signal();
                        }
                    });
                });
            });

            return promise;
        }
    })
});