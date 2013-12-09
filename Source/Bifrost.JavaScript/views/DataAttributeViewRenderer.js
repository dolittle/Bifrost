Bifrost.namespace("Bifrost.views", {
	DataAttributeViewRenderer : Bifrost.views.ViewRenderer.extend(function(viewFactory, pathResolvers, viewModelManager) {
	    var self = this;

	    this.viewFactory = viewFactory;
	    this.pathResolvers = pathResolvers;
	    this.viewModelManager = viewModelManager;

		this.canRender = function(element) {
		    return typeof $(element).data("view") !== "undefined";
		};

		this.render = function (element) {
		    var promise = Bifrost.execution.Promise.create();
		    var path = $(element).data("view");

		    if (self.pathResolvers.canResolve(element, path)) {
		        var actualPath = self.pathResolvers.resolve(element, path);
		        var view = self.viewFactory.createFrom(actualPath);

		        view.element = element;
		        view.load().continueWith(function (targetView) {
		            $(element).empty();
		            $(element).append(targetView.content);

		            if (self.viewModelManager.hasForView(actualPath)) {
		                var viewModelFile = Bifrost.Path.changeExtension(actualPath, "js");
		                $(element).attr("data-viewmodel-file", viewModelFile);
		                $(element).data("viewmodel-file", viewModelFile);
		            }

		            promise.signal(targetView);
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

