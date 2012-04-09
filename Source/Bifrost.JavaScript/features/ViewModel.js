Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function(window, undefined) {
	function ViewModel() {
		this.messenger = Bifrost.messaging.messenger;
		this.uri = Bifrost.Uri.create(window.location.href);

		if(typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
			History.Adapter.bind(window,"statechange", function() {
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