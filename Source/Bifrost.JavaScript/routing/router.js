Bifrost.namespace("Bifrost.routing");
Bifrost.routing.router = (function(window, undefined) {
	var self = this;
	this.routes = [];
	
	if(typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
		print("HISTORY");
		History.Adapter.bind(window,"statechange", function() {
			print("We have a change");
			$.each(this.routes, function(index, route) {
				route.isMatch();
			});
		});
	}
	
	return {
		register: function(route) {
			self.routes.push(route);
		},
		routes: this.routes
	}
})(window);