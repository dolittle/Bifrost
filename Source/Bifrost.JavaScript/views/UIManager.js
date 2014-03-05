Bifrost.namespace("Bifrost.views", {
	UIManager: Bifrost.Singleton(function(documentService) {
		var visitors = [];
		var visitorTypes = Bifrost.views.ElementVisitor.getExtenders();

		visitorTypes.forEach(function(type) {
			visitors.push(type.create());
		})

		this.handle = function (root) {
			documentService.traverseObjects(function(element) {
				visitors.forEach(function(visitor) {
					var actions = Bifrost.views.ElementVisitorResultActions.create();
					visitor.visit(element, actions);
				});
			}, root);
		};
	})
});
Bifrost.WellKnownTypesDependencyResolver.types.UIManager = Bifrost.views.UIManager;