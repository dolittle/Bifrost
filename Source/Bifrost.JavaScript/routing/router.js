Bifrost.namespace("Bifrost.routing");
Bifrost.routing.router = (function(window, undefined) {
	var self = this;
	this.routes = [];
	
	if(typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
		History.Adapter.bind(window,"statechange", function() {
			$.each(self.routes, function(index, route) {
				if( route.isMatch !== undefined && typeof route.isMatch == "function" ) {
					route.isMatch();
				}
			});
		});
	}
	
	return {
		reset: function() {
			this.routes = [];
		},
		register: function(route) {
			self.routes.push(route);
		},
		routes: this.routes
	}
})(window);