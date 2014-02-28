Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function(UIManager, viewModelManager, documentService, pathResolvers, viewFactory, viewLoader, element, viewUri, regionManager, allBindingsAccessor) {
        var self = this;
        var loaded = false;
        var view = ko.observable("<span></span>");

        this.data = function (key, value) {
            return {};
        };

        this.text = function (value) {
            if (loaded == false) {
                loaded = true;
                var uri = ko.utils.unwrapObservable(viewUri);
                if (Bifrost.isNullOrUndefined(uri) || uri === "") {
                    viewModelManager.masterViewModel.clearFor(element);
                    documentService.cleanChildrenOf(element);
                    view("<span></span>");
                    documentService.setViewUriOn(element, null);
                } else {
                    var existingUri = documentService.getViewUriFrom(element);
                    if (existingUri !== uri) {
                        viewModelManager.masterViewModel.clearFor(element);
                        documentService.cleanChildrenOf(element);
                        documentService.setViewUriOn(element, uri);

                        var viewModelParameters = allBindingsAccessor().viewModelParameters || null;
                        documentService.setViewModelParametersOn(element, viewModelParameters);

                        var actualPath = pathResolvers.resolve(element, viewUri);
                        var actualView = viewFactory.createFrom(actualPath);
                        actualView.element = element;
                        //var region = documentService.getRegionFor(element);

                        regionManager.getFor(actualView).continueWith(function (region) {

                            actualView.load(region).continueWith(function (loadedView) {
                                viewModelManager.masterViewModel.setFor(element, loadedView.viewModel);

                                var wrapper = document.createElement("div");
                                wrapper.innerHTML = loadedView.content;

                                UIManager.handle(wrapper);

                                loadedView.content = wrapper.innerHTML;

                                view(loadedView.content);
                            });
                        });
                    }
                }
            }

            var viewInstance = view();
            return viewInstance;
        };
    })
})