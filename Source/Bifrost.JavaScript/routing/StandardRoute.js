Bifrost.namespace("Bifrost.routing");
Bifrost.routing.StandardRoute = (function() {
	function StandardRoute(options) {
		var options = {
			name : "No Name",
			url : null,
			handler : null,
			defaultValues : {}
		}
	}
	
	return {
		create: function(options) {
			var route = new StandardRoute(options);
			return route;
		}
	}	
})();