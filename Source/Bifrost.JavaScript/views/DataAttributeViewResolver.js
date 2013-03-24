Bifrost.namespace("Bifrost.views", {
	DataAttributeViewResolver : Bifrost.views.viewResolver.extend(function() {
		var self = this;

		this.canResolve = function(element) {
		    return $(element).data("view") !== "unedefined";
		};

		this.resolve = function(element) {

		};
	})
});
if( typeof Bifrost.views.viewResolvers != "undefined" ) {
	Bifrost.views.viewResolvers.DataAttributeViewResolver = Bifrost.views.DataAttributeViewResolver;
}

