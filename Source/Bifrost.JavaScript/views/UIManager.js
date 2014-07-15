Bifrost.namespace("Bifrost.views", {
	UIManager: Bifrost.Singleton(function(documentService) {
	    var elementVisitorTypes = Bifrost.markup.ElementVisitor.getExtenders();
		var elementVisitors = [];
		var postBindingVisitorTypes = Bifrost.views.PostBindingVisitor.getExtenders();
		var postBindingVisitors = [];

		elementVisitorTypes.forEach(function (type) {
		    elementVisitors.push(type.create());
		});

		postBindingVisitorTypes.forEach(function (type) {
		    postBindingVisitors.push(type.create());
		});

		this.handle = function (root) {
			documentService.traverseObjects(function(element) {
				elementVisitors.forEach(function(visitor) {
				    var actions = Bifrost.markup.ElementVisitorResultActions.create();
					visitor.visit(element, actions);
				});
			}, root);
		};

		this.handlePostBinding = function (root) {
		    documentService.traverseObjects(function (element) {
		        postBindingVisitors.forEach(function (visitor) {
		            visitor.visit(element);
		        });
		    }, root);
		};
	})
});
Bifrost.WellKnownTypesDependencyResolver.types.UIManager = Bifrost.views.UIManager;