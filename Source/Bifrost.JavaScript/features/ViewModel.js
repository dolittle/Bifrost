Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function(window, undefined) {	
	function ViewModel() {
		var self = this;
		
		this.uriChangedSubscribers = [];
		this.activatedSubscribers = [];
		
		this.messenger = Bifrost.messaging.Messenger.global;
		this.uri = Bifrost.Uri.create(window.location.href);
		this.queryParameters = {
			define: function(parameters) {
				Bifrost.extend(this,parameters);
			}
		}
		
		this.uriChanged = function(callback) {
			self.uriChangedSubscribers.push(callback);
		}
		
		this.activated = function(callback) {
			self.activatedSubscribers.push(callback);
		}
		
		
		this.onUriChanged = function(uri) {
			self.uriChangedSubscribers.forEach(function(callback) {
				callback(uri);
			});
		}
		
		this.onActivated = function() {
			if( typeof self.handleUriState !== "undefined" ) {
				self.handleUriState();
			}
			
			self.activatedSubscribers.forEach(function(callback) {
				callback();
			});
		}

		if(typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
			this.handleUriState = function() {
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
				
				self.onUriChanged(self.uri);
			}
			
			History.Adapter.bind(window,"statechange", function() {
				self.handleUriState();
			});		
			
			$(function() {
				self.handleUriState();
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