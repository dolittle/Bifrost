Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function(window, undefined) {
	Bifrost.features.ViewModel = Bifrost.features.ViewModel || {
		baseFor: function() {}
	};
	
	
	function ViewModel() {
		var self = this;
		this.messenger = Bifrost.messaging.messenger;
		this.uri = Bifrost.Uri.create(window.location.href);
		this.queryParameters = {
			define: function(parameters) {
				Bifrost.extend(this,parameters);
			}
		};

		if(typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
			History.Adapter.bind(window,"statechange", function() {
				var state = History.getState();
				
				self.uri.setLocation(state.url);
				
				for( var parameter in self.uri.parameters ) {
					if( self.queryParameters.hasOwnProperty(parameter) && 
						typeof self.uri.parameters[parameter] != "function") {
						
						if( typeof self.queryParameters[parameter] == "function" ) {
							self.queryParameters[parameter](self.uri.parameters[parameter]);
						} else {
							self.queryParameters[parameter] = self.uri.parameters[parameter];
						}
					}
				}
			});
		}
	}
	
	return {
		baseFor : function(f) {
			if( typeof f === "function" ) {
				f.prototype = new ViewModel();
			}
		}
	};
})(window);