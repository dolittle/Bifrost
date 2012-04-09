Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function() {
	function ViewModel() {
		// this.messenger = Bifrost.messaging.messenger || {};
		
		// uri

	}
	
	return {
		baseFor : function(f) {
			if( typeof f === "function" ) {
				f.prototype = new ViewModel();
			}
		}
	};
})();