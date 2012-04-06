Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function() {
	var ViewModel = {
		
	}
	
	return {
		superFor : function(f) {
			if( typeof f === function ) {
				f.prototype = ViewModel;
			}
		}
	};
})();