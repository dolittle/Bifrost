Bifrost.namespace("Bifrost.views", {
	DataAttributeViewRenderer : Bifrost.views.ViewRenderer.extend(function(viewFactory, viewPathResolvers) {
	    var self = this;

	    this.viewFactory = viewFactory;
	    this.viewPathResolvers = viewPathResolvers;

		this.canRender = function(element) {
		    return typeof $(element).data("view") !== "undefined";
		};

		this.render = function (element) {
		    var promise = Bifrost.execution.Promise.create();
		    var path = $(element).data("view");

		    if (self.viewPathResolvers.canResolve(element, path)) {
		        var actualPath = self.viewPathResolvers.resolve(element, path);
		        var view = self.viewFactory.createFrom(actualPath);

		        view.element = element;
		        view.load().continueWith(function (targetView) {
		            $(element).empty();
		            $(element).append(targetView.content);
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

