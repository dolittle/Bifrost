Bifrost.namespace("Bifrost.views", {
	NamingRootElementVisitor: Bifrost.views.ElementVisitor.extend(function() {
		this.visit = function(element, actions) {
			var namingRoot = Bifrost.views.NamingRoot.create();
			namingRoot.target = element;
			element.namingRoot = namingRoot;
		};
	})
});