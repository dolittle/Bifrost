Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function() {
	function ViewModel() {
		
		this.coolShit = function() {
			print("Cool as hell");
		}
	}
	
	return {
		baseFor : function(f) {
			if( typeof f === "function" ) {
				f.prototype = new ViewModel();
			}
		}
	};
})();