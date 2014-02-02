Bifrost.namespace("Bifrost.views", {
	DataAttributeViewRenderer : Bifrost.views.ViewRenderer.extend(function(viewFactory, pathResolvers, viewModelManager, regionManager, documentService) {
	    var self = this;

	    this.viewFactory = viewFactory;
	    this.pathResolvers = pathResolvers;
	    this.viewModelManager = viewModelManager;

		this.canRender = function(element) {
		    return documentService.hasViewUri(element);
		};

		this.render = function (element) {
		    var promise = Bifrost.execution.Promise.create();
		    var path = documentService.getViewUriFrom(element);

		    if (self.pathResolvers.canResolve(element, path)) {
		        var actualPath = self.pathResolvers.resolve(element, path);
		        var view = self.viewFactory.createFrom(actualPath);
		        element.view = view;

		        view.element = element;
		        regionManager.getFor(view).continueWith(function (region) {
		            view.load(region).continueWith(function (targetView) {
		                documentService.cleanChildrenOf(element);
		                $(element).empty();
		                $(element).append(targetView.content);

		                if (self.viewModelManager.hasForView(actualPath)) {
		                    var viewModelFile = Bifrost.Path.changeExtension(actualPath, "js");
		                    documentService.setViewModelFileOn(element, viewModelFile);
		                }

		                promise.signal(targetView);
		            });
		        });
		    } else {
                // Todo: throw an exception at this point! - Or something like 404.. 
		        promise.signal(null);
		    }

		    return promise;
		};
	})
});
if (typeof Bifrost.views.viewRenderers != "undefined") {
	Bifrost.views.viewRenderers.DataAttributeViewRenderer = Bifrost.views.DataAttributeViewRenderer;
}

