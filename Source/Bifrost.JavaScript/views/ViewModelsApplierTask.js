Bifrost.namespace("Bifrost.views", {
    ViewModelsApplierTask: Bifrost.views.ComposeTask.extend(function (root, masterViewModel, viewModelLoader, documentService, regionManager) {
        /// <summary>Represents a task for applying view models</summary>
        var self = this;

        function setViewModelBindingExpression(instance, target) {
            var viewModelFile = documentService.getViewModelFileFrom(target);
            documentService.setViewModelOn(target, instance);

            if (viewModelFile.indexOf(".") > 0) {
                viewModelFile = viewModelFile.substr(0, viewModelFile.indexOf("."));
            }

            var propertyName = viewModelFile.replaceAll("/", "");
            masterViewModel[propertyName] = ko.observable(instance);

            $(target).attr("data-bind", "viewModel: $data." + propertyName);

            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }


        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            var elements = documentService.getAllElementsWithViewModelFilesFrom(root);
            var loadedViewModels = 0;

            elements.forEach(function (element) {
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

                regionManager.getFor(view).continueWith(function (region) {
                    viewModelLoader.load(viewModelFile, region).continueWith(function (instance) {
                        documentService.setViewModelOn(element, instance);

                        loadedViewModels++;

                        if (loadedViewModels == elements.length) {
                            elements.forEach(function (elementToApplyBindingsTo) {
                                var viewModel = documentService.getViewModelFrom(elementToApplyBindingsTo);
                                setViewModelBindingExpression(viewModel, elementToApplyBindingsTo);
                            });


                            if (!documentService.pageHasViewModel(masterViewModel)) {
                                ko.applyBindings(masterViewModel);
                            } else {
                                ko.applyBindings(instance, element);
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