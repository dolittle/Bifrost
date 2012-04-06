Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function() {
	function ViewModel() {
	}
	
	return {
		baseFor : function(f) {
			if( typeof f === "function" ) {
				f.prototype = new ViewModel();
			}
		}
	};
})();